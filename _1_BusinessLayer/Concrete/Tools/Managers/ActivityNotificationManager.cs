using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.CommandHandler;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Tools.Managers
{
    public class ActivityNotificationManager
    {
        public AbstractGenericCommandHandler _commandHandler;
        public ActivityNotificationManager(AbstractGenericCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }
        public async Task<IdentityResult> CommitNotification(NotificationType notificationType, object entity, User? user, Bot? bot)
        {
            object sender = (object)user ?? (object)bot;
            if (sender == null)
                return IdentityResult.Failed(new UnexpectedError("Sender (User or Bot) must be provided"));

            IdentityResult result;

            switch (notificationType)
            {
                case NotificationType.PostLike when entity is Post post:
                    result = await CommitPostLikeNotification(post, sender);
                    if (!result.Succeeded)
                        return result;

                    break;

                case NotificationType.EntryLike when entity is Entry entry:
                    result = await CommitEntryLikeNotification(entry, sender);
                    break;

                case NotificationType.CreatingEntry when entity is Entry entry2:
                    result = await CommitCreatingEntryNotification(entry2, sender);
                    break;

                case NotificationType.CreatingPost when entity is Post post2:
                    result = await CommitCreatingPostNotification(post2, sender);
                    break;

                case NotificationType.GainedFollower when entity is Follow follow:
                    result = await CommitGainedFollowerNotification(follow, sender);
                    break;

                case NotificationType.BotActivity when entity is BotActivityType botActivity:
                    result = await CommitBotActivityNotification(botActivity, sender);
                    break;

                default:
                    result = IdentityResult.Failed(
                        new UnexpectedError($"Incompatible notification ({notificationType}) and entity ({entity?.GetType().Name ?? "null"})")
                    );
                    break;
            }

            return result;
        }



        internal async Task<IdentityResult> CommitPostLikeNotification(Post post, object sender)
        {
            var postOwnerNotification = new Notification
            {
                NotificationType = NotificationType.PostLike,
                DateTime = DateTime.UtcNow,
                AdditionalId = post.PostId,
                AdditionalInfo = post.Title,
                IsRead = false,
                OwnerUserId = post.OwnerUserId,
                FromUserId = sender is User u ? u.Id : null,
                FromBotId = sender is Bot b ? b.Id : null
            };
            await _commandHandler.ManuallyInsertAsync<Notification>(postOwnerNotification);
            return IdentityResult.Success;
        }

        internal async Task<IdentityResult> CommitEntryLikeNotification(Entry entry, object sender)
        {
            var entryOwnerNotification = new Notification
            {
                NotificationType = NotificationType.EntryLike,
                DateTime = DateTime.UtcNow,
                AdditionalId = entry.EntryId,
                AdditionalInfo = entry.Context,
                IsRead = false,
                OwnerUserId = entry.OwnerUserId,
                FromUserId = sender is User u ? u.Id : null,
                FromBotId = sender is Bot b ? b.Id : null
            };
            await _commandHandler.ManuallyInsertAsync<Notification>(entryOwnerNotification);
            await _commandHandler.SaveChangesAsync();
            return IdentityResult.Success;
        }

        internal async Task<IdentityResult> CommitCreatingEntryNotification(Entry entry, object sender)
        {
            var notifications = new List<Notification>();
            var followerIds = new List<Guid?>();
            if (sender is User user) { followerIds = user.Followers.Select(f => f.UserFollowerId).ToList(); }
            if (sender is Bot bot) { followerIds = bot.Followers.Select(f => f.UserFollowerId).ToList(); }
            {
                notifications.Add(new Notification
                {
                    NotificationType = NotificationType.CreatingEntry,
                    DateTime = DateTime.UtcNow,
                    AdditionalId = entry.EntryId,
                    AdditionalInfo = entry.Context,
                    IsRead = false,
                    OwnerUserId = entry.Post?.OwnerUserId,
                    FromUserId = sender is User u ? u.Id : null,
                    FromBotId = sender is Bot b ? b.Id : null
                });
            }
            foreach (var followerId in followerIds)
            {
                notifications.Add(new Notification
                {
                    NotificationType = NotificationType.CreatingEntry,
                    DateTime = DateTime.Now,
                    AdditionalId = entry.EntryId,
                    AdditionalInfo = entry.Context,
                    IsRead = false,
                    OwnerUserId = followerId,
                    FromUserId = sender is User u ? u.Id : null,
                    FromBotId = sender is Bot b ? b.Id : null
                });
            }
            await _commandHandler.ManuallyInsertRangeAsync<Notification>(notifications);
            await _commandHandler.SaveChangesAsync();
            return IdentityResult.Success;
        }

        internal async Task<IdentityResult> CommitCreatingPostNotification(Post post, object sender)
        {
            var notifications = new List<Notification>();
            var followerIds = new List<Guid?>();
            if (sender is User user) { followerIds = user.Followers.Select(f => f.UserFollowerId).ToList(); }
            if (sender is Bot bot) { followerIds = bot.Followers.Select(f => f.BotFollowerId).ToList(); }
            foreach (var followerId in followerIds)
            {
                notifications.Add(new Notification
                {
                    NotificationType = NotificationType.CreatingPost,
                    DateTime = DateTime.Now,
                    AdditionalId = post.PostId,
                    AdditionalInfo = post.Title,
                    IsRead = false,
                    OwnerUserId = followerId,
                    FromUserId = sender is User u ? u.Id : null,
                    FromBotId = sender is Bot b ? b.Id : null,
                });
            }
            await _commandHandler.ManuallyInsertRangeAsync<Notification>(notifications);
            await _commandHandler.SaveChangesAsync();
            return IdentityResult.Success;
        }

        internal async Task<IdentityResult> CommitGainedFollowerNotification(Follow follow, object sender)
        {
            if (follow.UserFollowedId == null)
                return IdentityResult.Failed(new ForbiddenError("Invalid follow"));
            var followedNotification = new Notification
            {
                NotificationType = NotificationType.GainedFollower,
                DateTime = DateTime.Now,
                AdditionalId = sender is User u ? u.Id : sender is Bot b ? b.Id : null,
                AdditionalInfo = sender is User uu ? uu.ProfileName : sender is Bot bb ? bb.BotProfileName : null,
                OwnerUserId = follow.UserFollowedId,
                FromUserId = sender is User uuu ? uuu.Id : null,
                FromBotId = sender is Bot bbb ? bbb.Id : null,
                IsRead = false
            };
            await _commandHandler.ManuallyInsertAsync<Notification>(followedNotification);
            await _commandHandler.SaveChangesAsync();
            return IdentityResult.Success;
        }


        internal async Task<IdentityResult> CommitBotActivityNotification(BotActivity botActivity, object sender)
        {
            await _commandHandler.ManuallyInsertAsync<Notification>(new Notification
            {
                NotificationType = NotificationType.BotActivity,
                BotActivityJson = botActivity,
                DateTime = DateTime.Now,
                IsRead = false,
                AdditionalId = null,
                AdditionalInfo = null,
                FromBotId = sender is Bot b ? b.Id : null,
                OwnerUserId = sender is Bot bb ? bb.ParentUserId : null,
            });
            await _commandHandler.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public ObjectIdentityResult<BotActivity> CommitBotActivities(BotActivityType botActivityType, object entity, Bot bot)
        {
            var additonalId = null as Guid?;
            var additionalInfo = null as string;
            var fromUserId = null as Guid?;
            var fromBotId = null as Guid?;

            switch (botActivityType)
            {
                case BotActivityType.BotLikedEntry when entity is Entry entry:
                    additonalId = entry.EntryId;
                    additionalInfo = entry.Context;
                    break;
                case BotActivityType.BotLikedPost when entity is Post post:
                    additonalId = post.PostId;
                    additionalInfo = post.Title;
                    break;
                case BotActivityType.BotPostLiked when entity is Post post:
                    additonalId = post.PostId;
                    additionalInfo = post.Title;
                    break;
                case BotActivityType.BotEntryLiked when entity is Entry entry:
                    additonalId = entry.EntryId;
                    additionalInfo = entry.Context;
                    break;
                case BotActivityType.BotCreatedEntry when entity is Entry entry:
                    additonalId = entry.EntryId;
                    additionalInfo = entry.Context;
                    break;
                case BotActivityType.BotCreatedPost when entity is Post post:
                    additonalId = post.PostId;
                    additionalInfo = post.Title;
                    break;
                case BotActivityType.BotGainedFollower when entity is Follow follow with { follow.BotFollowedId != null }:
                    additonalId = follow.UserFollowerId;
                    additionalInfo = follow.UserFollower?.ProfileName ?? follow.BotFollower?.BotProfileName;
                    break;
                case BotActivityType.BotStartedFollow when entity is Bot followedBot:
                    additonalId = followedBot.Id;
                    additionalInfo = followedBot.BotProfileName;
                case BotActivityType.BotCreatedChildBot:
                case BotActivityType.BotCreatedOpposingEntry:
                default:
                    return ObjectIdentityResult<BotActivity>.Failed(null, new[] { new UnexpectedError($"Unknown BotActivityType: {botActivityType}") });
            }
        }


        public void Publish()
        {
            // ...existing code...
        }
    }
}
