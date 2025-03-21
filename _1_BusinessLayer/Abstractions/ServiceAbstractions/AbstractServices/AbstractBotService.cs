using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Tools.BotManagers;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractBotService : IBotService
    {
        protected readonly AbstractBotRepository _botRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly BotManager _botManager;

        protected AbstractBotService(AbstractBotRepository botRepository, BotManager botManager, AbstractUserRepository userRepository)
        {
            _botRepository = botRepository;
            _botManager = botManager;
            _userRepository = userRepository;
        }

        public abstract Task<IdentityResult> CreateBot(int userId, CreateBotDto createBotDto);
        public abstract Task<IdentityResult> DeleteBot(int userId, int botId);
        public abstract Task<IdentityResult> DeployBot(int userId, int botId);
        public abstract Task<IdentityResult> EditBot(int userId, EditBotDto editBotDto);
        public abstract Task<ObjectIdentityResult<BotProfileDto>> GetBotProfile(int botId);
        public abstract Task<ObjectIdentityResult<BotProfileDto>> GetBotProfileFromBotActivity(int botActivityId);
    }
}
