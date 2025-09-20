using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;


namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class BotDatabaseReader
    {
        public readonly AbstractPostQueryHandler _postQueryHandler;
        public readonly AbstractEntryQueryHandler _entryQueryHandler;
        public readonly AbstractTrendingPostQueryHandler _trendingPostQueryHandler;
        public readonly AbstractUserQueryHandler _userQueryHandler;
        public readonly AbstractBotQueryHandler _botQueryHandler;
        public readonly AbstractBotMemoryLogHandler _botMemoryLogQueryHandler;
        public readonly AbstractNewsQueryHandler _newsQueryHandler;
        public readonly AbstractGenericCommandHandler _commandHandler;

        public BotDatabaseReader(
            AbstractBotQueryHandler botQueryHandler,
            AbstractEntryQueryHandler entryQueryHandler,
            AbstractTrendingPostQueryHandler trendingPostQueryHandler,
            AbstractUserQueryHandler userQueryHandler,
            AbstractPostQueryHandler postQueryHandler,
            AbstractBotMemoryLogHandler botMemoryLogQueryHandler,
            AbstractNewsQueryHandler newsQueryHandler,
            AbstractGenericCommandHandler commandHandler)
        {
            _botQueryHandler = botQueryHandler;
            _entryQueryHandler = entryQueryHandler;
            _trendingPostQueryHandler = trendingPostQueryHandler;
            _userQueryHandler = userQueryHandler;
            _postQueryHandler = postQueryHandler;
            _commandHandler = commandHandler;
            _botMemoryLogQueryHandler = botMemoryLogQueryHandler;
            _newsQueryHandler = newsQueryHandler;
        }


        public async Task<ObjectIdentityResult<DatabaseDataDto>> ReadContextData(BotActivityType activityType, Bot bot)
        {
            DatabaseDataDto databaseData = null;
            switch (activityType)
            {
                case BotActivityType.BotLikedEntry:
                    databaseData = await ReadLikedEntryContext(bot, activityType);
                    break;
                case BotActivityType.BotLikedPost:
                    databaseData = await ReadLikedPostContext(bot, activityType);
                    break;
                case BotActivityType.BotStartedFollow:
                    databaseData = await ReadStartedFollowContext(bot, activityType);
                    break;
                case BotActivityType.BotCreatedEntry:
                    databaseData = await ReadCreatedEntryContext(bot, activityType);
                    break;
                case BotActivityType.BotCreatedPost:
                    databaseData = await ReadCreatedPostContext(bot, activityType);
                    break;
                case BotActivityType.BotCreatedOpposingEntry:
                    databaseData = await ReadCreatedOpposingEntryContext(bot, activityType);
                    break;
                case BotActivityType.BotCreatedChildBot:
                    databaseData = new DatabaseDataDto { ActivityType = activityType };
                    break;
                default:
                    return ObjectIdentityResult<DatabaseDataDto>.Failed(null, new[] { new UnexpectedError("Unsupported activity type") });
            }

            if (bot.BotCapabilities.HasFlag(BotCapabilities.StrongBotMemory))
            {
                var memoryResult = await AddMemory(databaseData, bot);
                if (!memoryResult.Succeeded)
                    return memoryResult;
                databaseData = memoryResult.Data!;
            }
            return ObjectIdentityResult<DatabaseDataDto>.Succeded(databaseData);
        }

        private async Task<DatabaseDataDto> ReadLikedEntryContext(Bot bot, BotActivityType activityType) //considers interests
        {
            var databaseData = new DatabaseDataDto();
            var random = new Random();
            int takeCount = bot.BotCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence) ? 10 : 1;
            var entryCount = await _entryQueryHandler.ExportDirectlyAsync().CountAsync();
            var randomNumber = random.Next(0, entryCount);
            var entries = await _entryQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(takeCount).Include(e => e.Post));
            databaseData.Entries = entries;
            databaseData.ActivityType = activityType;
            return databaseData;
        }

        private async Task<DatabaseDataDto> ReadLikedPostContext(Bot bot, BotActivityType activityType) //considers interests
        {
            var databaseData = new DatabaseDataDto();
            var random = new Random();
            int takeCount = bot.BotCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence) ? 10 : 1;
            var postCount = await _postQueryHandler.ExportDirectlyAsync().CountAsync();
            var randomNumber = random.Next(0, postCount);
            var posts = await _postQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(takeCount));
            databaseData.Posts = posts;
            databaseData.ActivityType = activityType;
            return databaseData;
        }

        private async Task<DatabaseDataDto> ReadStartedFollowContext(Bot bot, BotActivityType activityType) //considers interests
        {
            var databaseData = new DatabaseDataDto();
            var random = new Random();
            var coinFlip = random.NextDouble();
            if (coinFlip < 0.5)
            {
                var botCount = await _botQueryHandler.ExportDirectlyAsync().CountAsync();
                var randomNumber = random.Next(0, botCount);
                var bots = await _botQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(3));
                databaseData.Bots = bots;
            }
            else
            {
                var userCount = await _userQueryHandler.ExportDirectlyAsync().CountAsync();
                var randomNumber = random.Next(0, userCount);
                var users = await _userQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(3));
                databaseData.Users = users;
            }
            databaseData.ActivityType = activityType;
            return databaseData;
        }

        private async Task<DatabaseDataDto> ReadCreatedEntryContext(Bot bot, BotActivityType activityType) //considers interests
        {
            var databaseData = new DatabaseDataDto();
            var random = new Random();
            int takeCount = bot.BotCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence) ? 10 : 1;
            var postCount = await _postQueryHandler.ExportDirectlyAsync().CountAsync();
            var randomNumber = random.Next(0, postCount);
            var posts = await _postQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(takeCount));
            databaseData.Posts = posts;
            databaseData.ActivityType = activityType;
            return databaseData;
        }

        private async Task<DatabaseDataDto> ReadCreatedPostContext(Bot bot, BotActivityType activityType) //considers interests
        {
            var databaseData = new DatabaseDataDto();
            var random = new Random();
            int takeCount = bot.BotCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence) ? 10 : 1;
            var newsCount = await _newsQueryHandler.ExportDirectlyAsync().CountAsync();
            var randomNumber = random.Next(0, newsCount);
            var news = await _newsQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(takeCount));
            databaseData.News = news;
            databaseData.ActivityType = activityType;
            return databaseData;
        }

        private async Task<DatabaseDataDto> ReadCreatedOpposingEntryContext(Bot bot, BotActivityType activityType) //considers interests
        {
            var databaseData = new DatabaseDataDto();
            var random = new Random();
            int takeCount = bot.BotCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence) ? 10 : 1;
            var entryCount = await _entryQueryHandler.ExportDirectlyAsync().CountAsync();
            var randomNumber = random.Next(0, entryCount);
            var entriesWithPostContext = await _entryQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(takeCount).Include(e => e.OwnerUser).Include(e => e.OwnerBot).Include(e => e.Post));
            databaseData.Entries = entriesWithPostContext;
            databaseData.ActivityType = activityType;
            return databaseData;
        }

        public async Task<ObjectIdentityResult<DatabaseDataDto>> AddMemory(DatabaseDataDto databaseData, Bot bot)
        {
            var botMemoryLogs = await _botMemoryLogQueryHandler.GetWithCustomSearchAsync(q => q.Where(b => b.BotId == bot.Id).OrderBy(b => b.DateTime).Take(4));
            databaseData.BotMemoryLogs = botMemoryLogs;
            return ObjectIdentityResult<DatabaseDataDto>.Succeded(databaseData);
        }
    }
}
