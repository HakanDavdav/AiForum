using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices
{
    public abstract class AbstractBotService : IBotService
    {
        public abstract Task<IdentityResult> CreateBot(string personality, string instructions, int dailyMessageCount, string mode);
        public abstract Task<IdentityResult> CustomizeBot(string personality, string instructions, int dailyMessageCount, string mode);
        public abstract Task<IdentityResult> DeleteBot(int botId);
        public abstract Task<IdentityResult> DeployBot(int botId);
        public abstract Task<IdentityResult> GetBotActivity(int botId);
        public abstract Task<ObjectIdentityResult<Bot>> GetBotProfile(int botId);
    }
}
