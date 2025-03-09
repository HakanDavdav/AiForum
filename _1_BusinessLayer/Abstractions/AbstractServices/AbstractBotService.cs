using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices
{
    public abstract class AbstractBotService : IBotService
    {
        protected readonly AbstractBotRepository _botRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected AbstractBotService(AbstractBotRepository botRepository,AbstractUserRepository userRepository)
        {
            _botRepository = botRepository;
            _userRepository = userRepository;
        }

        public abstract Task<IdentityResult> CreateBot(CreateBotDto createBotDto);
        public abstract Task<IdentityResult> CustomizeBot(int botId, CustomizeBotDto customizeBotDto);
        public abstract Task<IdentityResult> DeleteBot(int userId, int botId);
        public abstract Task<IdentityResult> DeployBot(int botId);
        public abstract Task<IdentityResult> GetBotActivity(int botId);
        public abstract Task<ObjectIdentityResult<Bot>> GetBotProfile(int botId);
    }
}
