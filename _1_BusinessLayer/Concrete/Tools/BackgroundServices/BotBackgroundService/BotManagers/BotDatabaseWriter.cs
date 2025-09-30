using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Requests;
using static _2_DataAccessLayer.Concrete.Enums.BotEnums.BotActivityTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Dtos;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;
using _1_BusinessLayer.Concrete.Tools.Factories;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.MessageBackgroundService;
using _2_DataAccessLayer.Concrete.Enums.BotEnums;
using _2_DataAccessLayer.Concrete.Enums.OtherEnums;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class BotDatabaseWriter
    {
        private readonly ILogger<BotDatabaseWriter> _logger;
        public AbstractGenericCommandHandler _commandHandler;
        public NotificationEventFactory _notificationEventFactory;
        public MailEventFactory _mailEventFactory;
        public QueueSender _queueSender;

        public BotDatabaseWriter(AbstractGenericCommandHandler commandHandler, ILogger<BotDatabaseWriter> logger, 
            NotificationEventFactory notificationEventFactory, MailEventFactory mailEventFactory,QueueSender queueSender)
        {
            _commandHandler = commandHandler;
            _logger = logger;
            _notificationEventFactory = notificationEventFactory;
            _mailEventFactory = mailEventFactory;   
            _queueSender = queueSender;
        }

        public async Task<IdentityResult> WriteOnDatabase(DatabaseDataDto databaseDataDto, Bot bot ,BotResponseDto botResponse)
        {
            if (bot == null)
                return IdentityResult.Failed(new NotFoundError("Bot is null"));
            if (botResponse == null)
                return IdentityResult.Failed(new ValidationError("BotResponseDto is null"));
            if (botResponse.Candidates == null || botResponse.Candidates.Count == 0)
                return IdentityResult.Failed(new ValidationError("BotResponseDto.Candidates is null or empty"));

            foreach (var candidate in botResponse.Candidates.ToList())
            {
                if (candidate?.Content?.Parts == null) continue;
                candidate.Content.Parts = candidate.Content.Parts.Where(p => p != null && p.Data != null).ToList();
            }

            IdentityResult result;
            switch (databaseDataDto.ActivityType)
            {
                case BotActivityType.BotCreatedEntry:
                    result = await WriteEntry(bot, botResponse);
                    break;
                case BotActivityType.BotCreatedOpposingEntry:
                    result = await WriteOpposingEntry(bot, botResponse);
                    break;
                case BotActivityType.BotCreatedPost:
                    result = await WritePost(bot, botResponse);
                    break;
                case BotActivityType.BotStartedFollow:
                    result = await WriteFollow(bot, botResponse);
                    break;
                case BotActivityType.BotLikedPost:
                    result = await WriteLikePost(bot, botResponse);
                    break;
                case BotActivityType.BotLikedEntry:
                    result = await WriteLikeEntry(bot, botResponse);
                    break;
                case BotActivityType.BotCreatedChildBot:
                    result = await WriteChildBot(bot, botResponse);
                    break;
                default:
                    result = IdentityResult.Failed(new UnexpectedError("Invalid BotActivityType"));
                    break;
            }
            return result;
        }

        private bool HasUsableParts(BotResponseDto botResponse) => botResponse.Candidates != null && botResponse.Candidates.Any(c => c?.Content?.Parts != null && c.Content.Parts.Any(p => p?.Data != null));

        private void LogCollection<T>(string label, IEnumerable<T> list, Func<T, string> projector)
        {
            var arr = list.Select(projector).ToList();
            _logger.LogInformation("[BotDatabaseWriter] {Label} Written Count={Count} Items={Items}", label, arr.Count, string.Join(" | ", arr));
        }

        public async Task<IdentityResult> WriteEntry(Bot bot, BotResponseDto botResponse)
        {
            if (!HasUsableParts(botResponse)) return IdentityResult.Success;
            var added = new List<Entry>();
            foreach (var candidate in botResponse.Candidates!)
            {
                if (candidate?.Content?.Parts == null) continue;
                foreach (var part in candidate.Content.Parts)
                {
                    if (part?.Data == null) continue;
                    var entry = new Entry
                    {
                        Content = part.Data.Context,
                        PostId = part.Data.postId,
                        OwnerBotId = bot.Id,
                        DateTime = DateTime.Now,
                        LikeCount = 0,
                    };
                    await _commandHandler.ManuallyInsertAsync(entry);
                    added.Add(entry);
                    var botActivity = new BotActivity
                    {
                        OwnerBotId = bot.Id,
                        BotActivityType = BotActivityType.BotCreatedEntry,
                        DateTime = DateTime.Now,
                        IsRead = false,
                        AdditionalInfo = entry.Content,
                        AdditionalId = entry.EntryId
                    };
                    var notification = new Notification
                    {
                        DateTime = DateTime.Now,
                        IsRead = false,
                        ActorUserOwnerId = bot.ParentUserId,
                        AdditionalId = entry.EntryId,
                        AdditionalInfo = entry.Content,
                        NotificationType = NotificationType.BotActivity,
                        FromBotId = bot.Id
                    };
                    await _commandHandler.ManuallyInsertAsync(notification);
                    await _commandHandler.ManuallyInsertAsync(botActivity);
                    await _commandHandler.SaveChangesAsync();
                    var notificationEvent = _notificationEventFactory.CreateNotificationEvents(null, bot, new List<int?> { bot.ParentUserId }, NotificationType.BotActivity, entry.Content, entry.EntryId);
                    var mailEvent = _mailEventFactory.CreateMailEvents(null, bot, new List<int?> { bot.ParentUserId }, MailType.BotActivity, entry.Content, entry.EntryId);
                    await _queueSender.MailQueueSendAsync(mailEvent);
                    await _queueSender.NotificationQueueSendAsync(notificationEvent);

                }
            }
            LogCollection("Entries", added, e => $"PostId:{e.PostId} Ctx:{e.Content?.Substring(0, Math.Min(30, e.Content.Length))}");
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> WriteOpposingEntry(Bot bot, BotResponseDto botResponse)
        {
            if (!HasUsableParts(botResponse)) return IdentityResult.Success;
            var added = new List<Entry>();
            foreach (var candidate in botResponse.Candidates!)
            {
                if (candidate?.Content?.Parts == null) continue;
                foreach (var part in candidate.Content.Parts)
                {
                    if (part?.Data == null) continue;
                    var entry = new Entry
                    {
                        Content = part.Data.Context,
                        PostId = part.Data.postId,
                        OwnerBotId = bot.Id,
                        DateTime = DateTime.Now,
                        LikeCount = 0,
                    };
                    bot.Entries.Add(entry);
                    added.Add(entry);
                }
            }
            await _commandHandler.SaveChangesAsync();
            LogCollection("Entries", added, e => $"PostId:{e.PostId} Ctx:{e.Content?.Substring(0, Math.Min(30, e.Content.Length))}");
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> WritePost(Bot bot, BotResponseDto botResponse)
        {
            if (!HasUsableParts(botResponse)) return IdentityResult.Success;
            var added = new List<Post>();
            foreach (var candidate in botResponse.Candidates!)
            {
                if (candidate?.Content?.Parts == null) continue;
                foreach (var part in candidate.Content.Parts)
                {
                    if (part?.Data == null) continue;
                    var post = new Post
                    {
                        Content = part.Data.Context,
                        OwnerBotId = bot.Id,
                        Title = part.Data.Title,
                        DateTime = DateTime.Now,
                        LikeCount = 0,
                        EntryCount = 0,
                    };
                    bot.Posts.Add(post);
                    added.Add(post);
                }
            }
            await _commandHandler.SaveChangesAsync();
            LogCollection("Posts", added, p => $"Title:{p.Title} Ctx:{p.Content?.Substring(0, Math.Min(30, p.Content.Length))}");
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> WriteFollow(Bot bot, BotResponseDto botResponse)
        {
            if (!HasUsableParts(botResponse)) return IdentityResult.Success;
            var added = new List<Follow>();
            foreach (var candidate in botResponse.Candidates!)
            {
                if (candidate?.Content?.Parts == null) continue;
                foreach (var part in candidate.Content.Parts)
                {
                    if (part?.Data == null) continue;
                    var follow = new Follow
                    {
                        BotFollowerId = bot.Id,
                        UserFollowedId = part.Data.FollowedUserId,
                        BotFollowedId = part.Data.FollowedBotId,
                        DateTime = DateTime.Now,
                    };
                    bot.Followed.Add(follow);
                    added.Add(follow);
                }
            }
            await _commandHandler.SaveChangesAsync();
            LogCollection("Follows", added, f => $"User:{f.UserFollowedId} Bot:{f.BotFollowedId}");
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> WriteLikePost(Bot bot, BotResponseDto botResponse)
        {
            if (!HasUsableParts(botResponse)) return IdentityResult.Success;
            var addedPostLikes = new List<Like>();
            foreach (var candidate in botResponse.Candidates!)
            {
                if (candidate?.Content?.Parts == null) continue;
                foreach (var part in candidate.Content.Parts)
                {
                    if (part?.Data == null) continue;
                    var like = new Like
                    {
                        OwnerBotId = bot.Id,
                        PostId = part.Data.LikedPostId,
                        DateTime = DateTime.Now,
                    };
                    addedPostLikes.Add(like);
                    await _commandHandler.ManuallyInsertAsync(like);
                }
            }
            await _commandHandler.SaveChangesAsync();
            LogCollection("PostLikes", addedPostLikes, l => $"PostId:{l.PostId}");
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> WriteLikeEntry(Bot bot, BotResponseDto botResponse)
        {
            if (!HasUsableParts(botResponse)) return IdentityResult.Success;
            var addedEntryLikes = new List<Like>();
            foreach (var candidate in botResponse.Candidates!)
            {
                if (candidate?.Content?.Parts == null) continue;
                foreach (var part in candidate.Content.Parts)
                {
                    if (part?.Data == null) continue;
                    var like = new Like
                    {
                        OwnerBotId = bot.Id,
                        EntryId = part.Data.LikedEntryId,
                        DateTime = DateTime.Now,
                    };
                    addedEntryLikes.Add(like);
                    await _commandHandler.ManuallyInsertAsync(like);
                }
            }
            await _commandHandler.SaveChangesAsync();
            LogCollection("EntryLikes", addedEntryLikes, l => $"EntryId:{l.EntryId}");
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> WriteChildBot(Bot bot, BotResponseDto botResponse)
        {
            if (!HasUsableParts(botResponse)) return IdentityResult.Success;
            var capabilitiesValues = Enum.GetValues(typeof(BotCapabilities))
                .Cast<BotCapabilities>()
                .Where(c => c != BotCapabilities.None)
                .ToList();
            var random = new Random();
            int count = random.Next(1, capabilitiesValues.Count + 1);
            var selectedCapabilities = capabilitiesValues
                .OrderBy(x => random.Next())
                .Take(count)
                .Aggregate(BotCapabilities.None, (acc, val) => acc | val);

            var added = new List<Bot>();
            foreach (var candidate in botResponse.Candidates!)
            {
                if (candidate?.Content?.Parts == null) continue;
                foreach (var part in candidate.Content.Parts)
                {
                    if (part?.Data == null) continue;
                    var childBot = new Bot
                    {
                        BotProfileName = part.Data.BotProfileName,
                        BotPersonality = part.Data.BotPersonality,
                        Instructions = part.Data.BotInstructions,
                        Bio = part.Data.Bio,
                        Interests = part.Data.Interests,
                        ParentBotId = bot.Id,
                        DateTime = DateTime.Now,
                        BotCapabilities = selectedCapabilities
                    };
                    bot.ChildBots.Add(childBot);
                    added.Add(childBot);
                }
            }
            await _commandHandler.SaveChangesAsync();
            LogCollection("ChildBots", added, b => $"Name:{b.BotProfileName}");
            return IdentityResult.Success;
        }
    }
}
