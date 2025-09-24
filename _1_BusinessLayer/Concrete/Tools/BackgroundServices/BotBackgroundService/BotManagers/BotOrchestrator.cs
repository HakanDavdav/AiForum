using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class BotOrchestrator
    {
        protected BotDatabaseReader _botDatabaseReader;
        protected BotApiCaller _botApiCaller;
        protected BotDatabaseWriter _botDatabaseWriter;
        protected BotResponseParser _botResponseParser;
        protected ProbabilitySet _probabilitySet;
        protected BotRequestBuilder _botRequestBodyBuilder;
        public BotOrchestrator(BotDatabaseReader botDatabaseReader, BotApiCaller botApiCaller,
            BotDatabaseWriter botDatabaseWriter, BotResponseParser botResponseParser, ProbabilitySet probabilitySet, BotRequestBuilder botRequestBodyBuilder)
        {
            _botDatabaseReader = botDatabaseReader;
            _botApiCaller = botApiCaller;
            _botDatabaseWriter = botDatabaseWriter;
            _botResponseParser = botResponseParser;
            _probabilitySet = probabilitySet;
            _botRequestBodyBuilder = botRequestBodyBuilder;
        }
        public async Task<IdentityResult> BotDoDailyOperationsAsync(Bot bot)
        {
            if(bot == null)
                return IdentityResult.Failed(new NotFoundError("Bot not found"));
            if(bot.DailyOperationCheck == true)
                return IdentityResult.Failed(new ForbiddenError("Bot has already done daily operations today"));
            
            var activityProbabilityResult = _probabilitySet.ConfigureActivityProbabilities(bot);
            if (activityProbabilityResult.Succeeded == false)
                return IdentityResult.Failed(activityProbabilityResult.Errors.ToArray());

            var topicProbabilityResult = _probabilitySet.ConfigureTopicProbabilities(bot);
            if (topicProbabilityResult.Succeeded == false)
                return IdentityResult.Failed(topicProbabilityResult.Errors.ToArray());

            var selectedActivityResult = await _probabilitySet.DetermineOperation();
            if (selectedActivityResult.Succeeded == false)
                return IdentityResult.Failed(selectedActivityResult.Errors.ToArray());

            var databaseDataResult = await _botDatabaseReader.ReadContextData(selectedActivityResult.Data, bot);
            if (databaseDataResult.Succeeded == false)
                return IdentityResult.Failed(databaseDataResult.Errors.ToArray());

            var botRequestBodyResult = _botRequestBodyBuilder.BuildBotRequest(bot,databaseDataResult.Data!);
                if (botRequestBodyResult.Succeeded == false)
                return IdentityResult.Failed(botRequestBodyResult.Errors.ToArray());

            var botApiCallResult = await _botApiCaller.MakeApiCallAsync(botRequestBodyResult.Data!);
            if (botApiCallResult.Succeeded == false)
                return IdentityResult.Failed(botApiCallResult.Errors.ToArray());




        }
    }
}
