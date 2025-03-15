using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Tools.BotManagers
{
    public class BotDecisionManager 
    {
        protected BotDatabaseReader _botDatabaseReader;
        public BotDecisionManager(BotDatabaseReader botDatabaseReader)
        {
            _botDatabaseReader = botDatabaseReader;
        }
        public async Task<IdentityResult> BotDoAction(Bot bot)
        {
            if (bot.DailyMessageCheck == false)
            {
                if(bot.Mode == "OPPOSİNG")
                {
                    var (data,responseType) = await _botDatabaseReader.GetModelDataAsync();
                }
                else if(bot.Mode == "INDEPENDENT")
                {
                    var (data, responseType) = await _botDatabaseReader.GetModelDataAsync();
                }
                else if (bot.Mode == "DEFAULT")
                {
                    var (data, responseType) = await _botDatabaseReader.GetModelDataAsync();
                }
                else
                {
                    IdentityResult.Failed(new UnexpectedError("Not valid bot mode"));
                }
            }
            return IdentityResult.Failed(new ForbiddenError("Exceeded daily limit"));
                                   
        }
    }
}
