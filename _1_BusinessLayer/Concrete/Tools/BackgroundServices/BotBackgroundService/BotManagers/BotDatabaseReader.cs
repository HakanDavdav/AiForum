using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Dtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums.BotEnums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static _2_DataAccessLayer.Concrete.Enums.BotEnums.BotActivityTypes;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class BotDatabaseReader
    {
        private readonly ILogger<BotDatabaseReader> _logger;
        public readonly AbstractPostQueryHandler _postQueryHandler;
        public readonly AbstractEntryQueryHandler _entryQueryHandler;
        public readonly AbstractTrendingPostQueryHandler _trendingPostQueryHandler;
        public readonly AbstractUserQueryHandler _userQueryHandler;
        public readonly AbstractBotQueryHandler _botQueryHandler;
        public readonly AbstractBotMemoryLogHandler _botMemoryLogQueryHandler;
        public readonly AbstractNewsQueryHandler _newsQueryHandler;
        public readonly AbstractGenericCommandHandler _commandHandler;

        public BotDatabaseReader(
            ILogger<BotDatabaseReader> logger,
            AbstractBotQueryHandler botQueryHandler,
            AbstractEntryQueryHandler entryQueryHandler,
            AbstractTrendingPostQueryHandler trendingPostQueryHandler,
            AbstractUserQueryHandler userQueryHandler,
            AbstractPostQueryHandler postQueryHandler,
            AbstractBotMemoryLogHandler botMemoryLogQueryHandler,
            AbstractNewsQueryHandler newsQueryHandler,
            AbstractGenericCommandHandler commandHandler)
        {
            _logger = logger;
            _botQueryHandler = botQueryHandler;
            _entryQueryHandler = entryQueryHandler;
            _trendingPostQueryHandler = trendingPostQueryHandler;
            _userQueryHandler = userQueryHandler;
            _postQueryHandler = postQueryHandler;
            _commandHandler = commandHandler;
            _botMemoryLogQueryHandler = botMemoryLogQueryHandler;
            _newsQueryHandler = newsQueryHandler;
        }

        private void LogEntityList<T>(string label, IEnumerable<T>? items, Func<T,int> idSelector)
        {
            if (items == null) { _logger.LogInformation("{Label}: none", label); return; }
            var list = items.ToList();
            var ids = list.Select(idSelector).ToArray();
            _logger.LogInformation("{Label}: Count={Count}, Ids=[{Ids}]", label, list.Count, string.Join(',', ids));
        }

        public async Task<ObjectIdentityResult<DatabaseDataDto>> ReadDatabase(DatabaseDataDto databaseDataDto, Bot bot)
        {
            var activityType = databaseDataDto.ActivityType;
            switch (activityType)
            {
                case BotActivityType.BotLikedEntry:
                    await ReadLikedEntryContext(databaseDataDto, bot);
                    break;
                case BotActivityType.BotLikedPost:
                    await ReadLikedPostContext(databaseDataDto, bot);
                    break;
                case BotActivityType.BotStartedFollow:
                    await ReadStartedFollowContext(databaseDataDto, bot);
                    break;
                case BotActivityType.BotCreatedEntry:
                    await ReadCreatedEntryContext(databaseDataDto, bot);
                    break;
                case BotActivityType.BotCreatedPost:
                    await ReadCreatedPostContext(databaseDataDto, bot);
                    break;
                case BotActivityType.BotCreatedOpposingEntry:
                    await ReadCreatedOpposingEntryContext(databaseDataDto, bot);
                    break;
                case BotActivityType.BotCreatedChildBot:
                    break;
                default:
                    _logger.LogWarning("Unsupported activity type {ActivityType} for ActorId {ActorId}", activityType, bot.Id);
                    return ObjectIdentityResult<DatabaseDataDto>.Failed(null, new[] { new UnexpectedError("Unsupported activity type") });
            }

            if (databaseDataDto != null)
            {
                LogEntityList("Posts", databaseDataDto.Posts, p => p.PostId);
                LogEntityList("Entries", databaseDataDto.Entries, e => e.EntryId);
                LogEntityList("Users", databaseDataDto.Users, u => u.ActorId);
                LogEntityList("Bots", databaseDataDto.Bots, b => b.Id);
                LogEntityList("News", databaseDataDto.News, n => n.NewsId);
            }

            _logger.LogInformation("BuildRequest finished: ActivityType={ActivityType}, ActorId={ActorId}", activityType, bot.Id);

            if (bot.BotCapabilities.HasFlag(BotCapabilities.StrongBotMemory))
            {
                var memoryResult = await AddMemory(databaseDataDto, bot);
                if (!memoryResult.Succeeded)
                {
                    _logger.LogWarning("AddMemory failed for ActorId={ActorId}", bot.Id);
                    return memoryResult;
                }
                databaseDataDto = memoryResult.Data!;
                LogEntityList("MemoryLogs", databaseDataDto.BotMemoryLogs, m => m.BotMemoryLogId);
            }
            return ObjectIdentityResult<DatabaseDataDto>.Succeded(databaseDataDto);
        }

        private async Task ReadLikedEntryContext(DatabaseDataDto databaseData, Bot bot)
        {
            var random = new Random();
            int takeCount = bot.BotCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence) ? 10 : 1;
            var entryCount = await _entryQueryHandler.ExportDirectlyAsync().CountAsync();
            var randomNumber = random.Next(0, entryCount);
            var entries = await _entryQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(takeCount).Include(e => e.Post));
            databaseData.Entries = entries;
            _logger.LogInformation("ReadLikedEntryContext: ActorId={ActorId}, RetrievedEntries={Count}", bot.Id, entries.Count);
        }

        private async Task ReadLikedPostContext(DatabaseDataDto databaseData, Bot bot)
        {
            var random = new Random();
            int takeCount = bot.BotCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence) ? 10 : 1;
            var postCount = await _postQueryHandler.ExportDirectlyAsync().CountAsync();
            var randomNumber = random.Next(0, postCount);
            var posts = await _postQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(takeCount));
            databaseData.Posts = posts;
            _logger.LogInformation("ReadLikedPostContext: ActorId={ActorId}, RetrievedPosts={Count}", bot.Id, posts.Count);
        }

        private async Task ReadStartedFollowContext(DatabaseDataDto databaseData, Bot bot)
        {
            var random = new Random();
            var coinFlip = random.NextDouble();
            if (coinFlip < 0.5)
            {
                var botCount = await _botQueryHandler.ExportDirectlyAsync().CountAsync();
                var randomNumber = random.Next(0, botCount);
                var bots = await _botQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(3));
                databaseData.Bots = bots;
                _logger.LogInformation("ReadStartedFollowContext: ActorId={ActorId}, RetrievedBots={Count}", bot.Id, bots.Count);
            }
            else
            {
                var userCount = await _userQueryHandler.ExportDirectlyAsync().CountAsync();
                var randomNumber = random.Next(0, userCount);
                var users = await _userQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(3));
                databaseData.Users = users;
                _logger.LogInformation("ReadStartedFollowContext: ActorId={ActorId}, RetrievedUsers={Count}", bot.Id, users.Count);
            }
        }

        private async Task ReadCreatedEntryContext(DatabaseDataDto databaseData, Bot bot)
        {
            var random = new Random();
            int takeCount = bot.BotCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence) ? 10 : 1;
            var postCount = await _postQueryHandler.ExportDirectlyAsync().CountAsync();
            var randomNumber = random.Next(0, postCount);
            var posts = await _postQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(takeCount));
            databaseData.Posts = posts;
            _logger.LogInformation("ReadCreatedEntryContext: ActorId={ActorId}, RetrievedPosts={Count}", bot.Id, posts.Count);
        }

        private async Task ReadCreatedPostContext(DatabaseDataDto databaseData, Bot bot)
        {
            var random = new Random();
            int takeCount = bot.BotCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence) ? 10 : 1;
            var newsCount = await _newsQueryHandler.ExportDirectlyAsync().CountAsync();
            var randomNumber = random.Next(0, newsCount);
            var news = await _newsQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(takeCount));
            databaseData.News = news;
            _logger.LogInformation("ReadCreatedPostContext: ActorId={ActorId}, RetrievedNews={Count}", bot.Id, news.Count);
        }

        private async Task ReadCreatedOpposingEntryContext(DatabaseDataDto databaseData, Bot bot)
        {
            var random = new Random();
            int takeCount = bot.BotCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence) ? 10 : 1;
            var entryCount = await _entryQueryHandler.ExportDirectlyAsync().CountAsync();
            var randomNumber = random.Next(0, entryCount);
            var entriesWithPostContext = await _entryQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(takeCount).Include(e => e.OwnerUser).Include(e => e.OwnerBot).Include(e => e.Post));
            databaseData.Entries = entriesWithPostContext;
            _logger.LogInformation("ReadCreatedOpposingEntryContext: ActorId={ActorId}, RetrievedEntries={Count}", bot.Id, entriesWithPostContext.Count);
        }

        public async Task<ObjectIdentityResult<DatabaseDataDto>> AddMemory(DatabaseDataDto databaseData, Bot bot)
        {
            var botMemoryLogs = await _botMemoryLogQueryHandler.GetWithCustomSearchAsync(q => q.Where(b => b.BotId == bot.Id).OrderBy(b => b.DateTime).Take(4));
            databaseData.BotMemoryLogs = botMemoryLogs;
            _logger.LogInformation("AddMemory: ActorId={ActorId}, RetrievedMemoryLogs={Count}", bot.Id, botMemoryLogs.Count);
            return ObjectIdentityResult<DatabaseDataDto>.Succeded(databaseData);
        }
    }
}
