using System;
using System.Collections.Generic;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class ProbabilitySet
    {
        private readonly BotApiCaller _botApiCaller;
        private readonly BotResponseParser _botResponseParser;
        private Dictionary<BotActivityType, decimal> _activityProbabilities;
        private Dictionary<TopicTypes, decimal> _topicProbabilities;

        public ProbabilitySet(BotApiCaller botApiCaller, BotResponseParser botResponseParser)
        {
            _botApiCaller = botApiCaller;
            _botResponseParser = botResponseParser;
            _activityProbabilities = new Dictionary<BotActivityType, decimal>();
            _topicProbabilities = new Dictionary<TopicTypes, decimal>();
        }

        public async Task<ObjectIdentityResult<BotActivityType>> DetermineOperation()
        {
            var activityProbabilities = _activityProbabilities;
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
            if (selectedType == null)
                return ObjectIdentityResult<BotActivityType>.Failed(default, new[] { new UnexpectedError("Failed to determine activity type") });
            return ObjectIdentityResult<BotActivityType>.Succeded(selectedType.Value);
        }

        public IdentityResult ConfigureActivityProbabilities(Bot bot)
        {
            decimal likedEntry = 3m;
            decimal likedPost = 1.5m;
            decimal createdEntry = 5m;
            decimal createdPost = 1m;
            decimal startedFollow = 2m;
            decimal createdOpposingEntry = 2.5m;
            decimal createdChildBot = 0.5m;

            var probabilities = new Dictionary<BotActivityType, decimal>();

            if (bot.BotMode == BotModes.Opposing)
            {
                probabilities[BotActivityType.BotLikedEntry] = likedEntry;
                probabilities[BotActivityType.BotLikedPost] = likedPost;
                probabilities[BotActivityType.BotCreatedEntry] = createdEntry;
                probabilities[BotActivityType.BotCreatedPost] = createdPost;
                probabilities[BotActivityType.BotStartedFollow] = startedFollow;
                probabilities[BotActivityType.BotCreatedOpposingEntry] = createdOpposingEntry * 2m;
                probabilities[BotActivityType.BotCreatedChildBot] = createdChildBot;
            }
            else if (bot.BotMode == BotModes.Creator)
            {
                probabilities[BotActivityType.BotLikedEntry] = likedEntry;
                probabilities[BotActivityType.BotLikedPost] = likedPost;
                probabilities[BotActivityType.BotCreatedEntry] = createdEntry * 2m;
                probabilities[BotActivityType.BotCreatedPost] = createdPost * 2m;
                probabilities[BotActivityType.BotStartedFollow] = startedFollow;
                probabilities[BotActivityType.BotCreatedOpposingEntry] = createdOpposingEntry * 0.5m;
                probabilities[BotActivityType.BotCreatedChildBot] = createdChildBot;
            }
            else if (bot.BotMode == BotModes.Default)
            {
                probabilities[BotActivityType.BotLikedEntry] = likedEntry;
                probabilities[BotActivityType.BotLikedPost] = likedPost;
                probabilities[BotActivityType.BotCreatedEntry] = createdEntry;
                probabilities[BotActivityType.BotCreatedPost] = createdPost;
                probabilities[BotActivityType.BotStartedFollow] = startedFollow;
                probabilities[BotActivityType.BotCreatedOpposingEntry] = createdOpposingEntry;
                probabilities[BotActivityType.BotCreatedChildBot] = createdChildBot;
            }
            else
            {
                return IdentityResult.Failed(new UnexpectedError("Unexpected BotModType"));
            }

            _activityProbabilities = probabilities;
            return IdentityResult.Success;
        }

        public IdentityResult ConfigureTopicProbabilities(Bot bot)
        {
            if (bot.BotCapabilities.HasFlag(BotCapabilities.AdvancedIntelligence))
            {
                var topicObjectResult = _botApiCaller.TopicPreferenceApiCallAsync(bot);
                if (topicObjectResult.Succeeded == false)
                    return topicObjectResult;
                _topicProbabilities = _botResponseParser.ParseTopicPreferencesResponse(topicObjectResult.Data!);
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new ForbiddenError("Bot does not have advanced intelligence"));
            }
        }

        public IdentityResult ConfigureProbabilities(Bot bot)
        {
            var activityResult = ConfigureActivityProbabilities(bot);
            var topicResult = ConfigureTopicProbabilities(bot);
            if (!activityResult.Succeeded)
                return activityResult;
            if(!topicResult.Succeeded)
                return topicResult;
            return IdentityResult.Success;
        }

        public Dictionary<BotActivityType, decimal> GetAllActivityProbabilities()
        {
            return new Dictionary<BotActivityType, decimal>(_activityProbabilities);
        }

        public Dictionary<TopicTypes, decimal> GetAllTopicProbabilities()
        {
            return new Dictionary<TopicTypes, decimal>(_topicProbabilities);
        }

        // TODO: Buraya botCapabilities bazlı olasılıkları ekle
        // Örneğin:
        // if (botCapabilities.HasFlag(AdditionalBotCapabilityTypes.StrongBotMemory))
        // {
        //     _activityProbabilities[BotActivityType.CreateEntry] = 0.5m;
        // }
    }
}
