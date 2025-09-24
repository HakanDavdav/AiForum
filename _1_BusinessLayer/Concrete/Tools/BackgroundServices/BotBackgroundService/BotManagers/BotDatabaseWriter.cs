using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Requests;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class BotDatabaseWriter
    {
        public AbstractGenericCommandHandler _commandHandler;

        public BotDatabaseWriter(AbstractGenericCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public Task<Notification> WriteOnDatabase(Bot bot, BotResponseDto botResponse, DatabaseDataDto databaseDataDto)
        {
            switch (databaseDataDto.ActivityType)
            {
                case BotActivityType.BotCreatedEntry:
                    return WriteEntry(bot, botResponse);
                case BotActivityType.BotCreatedOpposingEntry:
                    return WriteOpposingEntry(bot, botResponse);
                case BotActivityType.BotCreatedPost:
                    return WritePost(bot, botResponse);
                case BotActivityType.BotStartedFollow:
                    return WriteFollow(bot, botResponse);
                case BotActivityType.BotLikedPost:
                    return WriteLikePost(bot, botResponse);
                case BotActivityType.BotLikedEntry:
                    return WriteLikeEntry(bot, botResponse);
                case BotActivityType.BotCreatedChildBot:
                    return WriteChildBot(bot, botResponse);
                default:
                    throw new ArgumentException("Invalid BotActivityType");
            }
        }

        public async Task<IdentityResult> WriteEntry(Bot bot, BotResponseDto botResponse)
        {
            foreach (var candidate in botResponse.Candidates)
            {
                foreach (var part in candidate.Content.Parts)
                {
                    var entry = new Entry
                    {
                        Context = part.Data.Context,
                        PostId = part.Data.postId,
                        OwnerBotId = bot.Id,
                        DateTime = DateTime.Now,
                        LikeCount = 0,
                    };
                    bot.Entries.Add(entry);
                }
            }
            await _commandHandler.SaveChangesAsync();
            return IdentityResult.Success;

        }
        public async Task<IdentityResult> WriteOpposingEntry(Bot bot, BotResponseDto botResponse)
        {
            foreach (var candidate in botResponse.Candidates)
            {
                foreach (var part in candidate.Content.Parts)
                {
                    var entry = new Entry
                    {
                        Context = part.Data.Context,
                        PostId = part.Data.postId,
                        OwnerBotId = bot.Id,
                        DateTime = DateTime.Now,
                        LikeCount = 0,
                    };
                    bot.Entries.Add(entry);
                }
            }
            await _commandHandler.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> WritePost(Bot bot, BotResponseDto botResponse)
        {
            foreach (var candidate in botResponse.Candidates)
            {
                foreach (var part in candidate.Content.Parts)
                {
                    var post = new Post
                    {
                        Context = part.Data.Context,
                        OwnerBotId = bot.Id,
                        Title = part.Data.Title,
                        DateTime = DateTime.Now,
                        LikeCount = 0,
                        EntryCount = 0,
                       
                    };
                    bot.Posts.Add(post);

                }
                await _commandHandler.SaveChangesAsync();
                return IdentityResult.Success;
            }
        }
        public async Task<IdentityResult> WriteFollow(Bot bot, BotResponseDto botResponse)
        {
            foreach (var candidate in botResponse.Candidates)
            {
                foreach (var parts in candidate.Content.Parts)
                {
                    var follow = new Follow
                    {
                        BotFollowerId = bot.Id,
                        UserFollowedId = parts.Data.FollowedUserId,
                        BotFollowedId = parts.Data.FollowedBotId,
                    };
                }
            }
        }
        public async Task<IdentityResult> WriteLikePost(Bot bot, BotResponseDto botResponse)
        {
            throw new NotImplementedException();
        }
        public async Task<IdentityResult> WriteLikeEntry(Bot bot, BotResponseDto botResponse)
        {
            throw new NotImplementedException();
        }
        public async Task<IdentityResult> WriteChildBot(Bot bot, BotResponseDto botResponse)
        {
            throw new NotImplementedException();
        }
    }
}
