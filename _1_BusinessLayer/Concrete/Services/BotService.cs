using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class BotService : AbstractBotService
    {
        public override Task<IdentityResult> CreateBot(string personality, string instructions, int dailyMessageCount, string mode)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> CustomizeBot(string personality, string instructions, int dailyMessageCount, string mode)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> DeleteBot(int botId)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> DeployBot(int botId)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> GetBotActivity(int botId)
        {
            throw new NotImplementedException();
        }

        public override Task<ObjectIdentityResult<Bot>> GetBotProfile(int botId)
        {
            throw new NotImplementedException();
        }
    }
}
