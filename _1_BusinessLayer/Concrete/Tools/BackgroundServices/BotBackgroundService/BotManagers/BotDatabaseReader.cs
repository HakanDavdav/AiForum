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
        public readonly AbstractTrendingPostQueryHandler _newsQueryHandler;
        public readonly AbstractUserQueryHandler _userQueryHandler;
        public readonly AbstractBotQueryHandler _botQueryHandler;
        public readonly AbstractBotMemoryLogHandler _botMemoryLogQueryHandler;
        public readonly AbstractGenericCommandHandler _commandHandler;

        public BotDatabaseReader(
            AbstractBotQueryHandler botQueryHandler,
            AbstractEntryQueryHandler entryQueryHandler,
            AbstractTrendingPostQueryHandler newsQueryHandler,
            AbstractUserQueryHandler userQueryHandler,
            AbstractPostQueryHandler postQueryHandler,
            AbstractBotMemoryLogHandler botMemoryLogQueryHandler,
            AbstractGenericCommandHandler commandHandler)
        {
            _botQueryHandler = botQueryHandler;
            _entryQueryHandler = entryQueryHandler;
            _newsQueryHandler = newsQueryHandler;
            _userQueryHandler = userQueryHandler;
            _postQueryHandler = postQueryHandler;
            _commandHandler = commandHandler;
            _botMemoryLogQueryHandler = botMemoryLogQueryHandler;
        }

        public async Task<ObjectIdentityResult<BotActivityType>> DetermineOperation(ProbabilitySet probabilitySet)
        {
            var activityProbabilities = probabilitySet.GetAllActivityProbabilities();
            if (activityProbabilities == null || activityProbabilities.Count == 0)
                return ObjectIdentityResult<BotActivityType>.Failed(default, new[] { new UnexpectedError("No activity probabilities defined") });

            // Weighted random selection
            var total = activityProbabilities.Values.Sum();
            var rand = new Random();
            var pick = rand.NextDouble() * (double)total;
            double cumulative = 0;
            BotActivityType? selectedType = null;
            foreach (var kvp in activityProbabilities)
            {
                cumulative += (double)kvp.Value;
                if (pick <= cumulative)
                {
                    selectedType = kvp.Key;
                    break;
                }
            }
            if(selectedType == null)
                return ObjectIdentityResult<BotActivityType>.Failed(default, new[] { new UnexpectedError("Failed to determine activity type") });
            return ObjectIdentityResult<BotActivityType>.Succeded(selectedType.Value);
        }

        public async Task<ObjectIdentityResult<ExpandoObject>> ReadData(BotActivityType activityType,BotCapabilities botCapabilities)
        {
            var random = new Random();
            if (activityType == BotActivityType.BotLikedEntry) 
            {
                if (botCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence))
                {

                }
                if (botCapabilities.HasFlag(BotCapabilities.StrongBotMemory))
                {

                }
                else
                {

                }

            
            }

            if(activityType == BotActivityType.BotLikedPost) 
            {
                dynamic obj = new ExpandoObject();
                if (botCapabilities == BotCapabilities.None)
                {

                    var postCount = await _newsQueryHandler.ExportDirectlyAsync().CountAsync();
                    var randomNumber = random.Next(0, postCount);
                    var post = await _newsQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(1));
                    obj.Posts = post;
                    obj.AdvancedIntelligence = false;
                    obj.StrongBotMemory = false;
                }
                if (botCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence))
                {
                    var postCount = await _postQueryHandler.ExportDirectlyAsync().CountAsync();
                    var randomNumber = random.Next(0, postCount);
                    var posts = await _postQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(10));
                    obj.Posts = posts;
                    obj.AdvancedIntelligence = true;
                }
                if (botCapabilities.HasFlag(BotCapabilities.StrongBotMemory))
                {
                    var botMemoryLogs = await _botMemoryLogQueryHandler.GetWithCustomSearchAsync(q => q.OrderBy(b => b.DateTime).Take(4));
                    obj.BotMemoryLogs = botMemoryLogs;
                    obj.StrongBotMemory = true;

                }
                return ObjectIdentityResult<ExpandoObject>.Succeded(obj);
            }

            if(activityType == BotActivityType.BotStartedFollow) 
            { 
                var botCount = await _botQueryHandler.ExportDirectlyAsync().CountAsync();
                var randomNumber = random.Next(0, botCount);
                var bots = await _botQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(3));
            }

            if (activityType == BotActivityType.BotCreatedEntry)
            {
                if (botCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence))
                {
                    var postCount = await _postQueryHandler.ExportDirectlyAsync().CountAsync();
                    var randomNumber = random.Next(0, postCount);
                    var posts = await _postQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(10));
                    return ObjectIdentityResult<Object>.Succeded(posts);
                }
                else
                {
                    var postCount = await _newsQueryHandler.ExportDirectlyAsync().CountAsync();
                    var randomNumber = random.Next(0, postCount);
                    var post = await _newsQueryHandler.GetWithCustomSearchAsync(q => q.Skip(randomNumber).Take(1));
                    return ObjectIdentityResult<Object>.Succeded(post);
                }
            }


            if(activityType == BotActivityType.BotCreatedPost) { }

            if(activityType == BotActivityType.BotCreatedOpposingEntry) { }
        }
    }
}
