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
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Dtos;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class BotOrchestrator
    {
        protected BotDatabaseReader _botDatabaseReader;
        protected BotApiCaller _botApiCaller;
        protected BotDatabaseWriter _botDatabaseWriter;
        protected ProbabilitySet _probabilitySet;
        protected BotRequestBodyBuilder _botRequestBodyBuilder;
        public BotOrchestrator(BotDatabaseReader botDatabaseReader, BotApiCaller botApiCaller,
            BotDatabaseWriter botDatabaseWriter, ProbabilitySet probabilitySet, BotRequestBodyBuilder botRequestBodyBuilder)
        {
            _botDatabaseReader = botDatabaseReader;
            _botApiCaller = botApiCaller;
            _botDatabaseWriter = botDatabaseWriter;
            _probabilitySet = probabilitySet;
            _botRequestBodyBuilder = botRequestBodyBuilder;
        }
        public async Task<IdentityResult> BotDoDailyOperationsAsync(Bot bot)
        {
            if(bot == null)
                return IdentityResult.Failed(new NotFoundError("Bot not found"));
            if(bot.DailyOperationCheck == true)
                return IdentityResult.Failed(new ForbiddenError("Bot has already done daily operations today"));

            var databaseDataDto = new DatabaseDataDto();

            var selectedActivityResult = await _probabilitySet.DetermineOperation(databaseDataDto, bot);
            if (selectedActivityResult.Succeeded == false)
                return IdentityResult.Failed(selectedActivityResult.Errors.ToArray());

            var databaseDataResult = await _botDatabaseReader.ReadDatabase(databaseDataDto, bot);
            if (databaseDataResult.Succeeded == false)
                return IdentityResult.Failed(databaseDataResult.Errors.ToArray());

            var botRequestBodyResult = _botRequestBodyBuilder.BuildRequest(databaseDataDto, bot);
            if (botRequestBodyResult.Succeeded == false)
                return IdentityResult.Failed(botRequestBodyResult.Errors.ToArray());

            var botApiCallResult = await _botApiCaller.MakeApiCallAsync(botRequestBodyResult.Data!);
            if (botApiCallResult.Succeeded == false)
                return IdentityResult.Failed(botApiCallResult.Errors.ToArray());

            var databaseWriteResult = await _botDatabaseWriter.WriteOnDatabase(databaseDataDto, bot, botApiCallResult.Data!);
            if( databaseWriteResult.Succeeded == false)
                return IdentityResult.Failed(databaseWriteResult.Errors.ToArray());


        }
    }
}
