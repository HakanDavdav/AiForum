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
    public class BotDeployManager
    {
        protected BotDatabaseReader _botDatabaseReader;
        protected BotApiCaller _botApiCaller;
        protected BotDatabaseWriter _botDatabaseWriter;
        protected BotResponseParser _botResponseParser;
        public BotDeployManager(BotDatabaseReader botDatabaseReader, BotApiCaller botApiCaller,
            BotDatabaseWriter botDatabaseWriter, BotResponseParser botResponseParser)
        {
            _botDatabaseReader = botDatabaseReader;
            _botApiCaller = botApiCaller;
            _botDatabaseWriter = botDatabaseWriter;
            _botResponseParser = botResponseParser;
        }
        public async Task<IdentityResult> BotDoDailyOperationsAsync(Bot bot)
        {
            if(bot == null)
                return IdentityResult.Failed(new NotFoundError("Bot not found"));
            if(bot.DailyOperationCheck == true)
                return IdentityResult.Failed(new ForbiddenError("Bot has already done daily operations today"));
            var data = await _botDatabaseReader.GetModelDataAsync(bot);


        }
    }
}
