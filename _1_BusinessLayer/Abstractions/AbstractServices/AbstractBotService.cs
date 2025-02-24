using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Abstractions.MainAbstractServices
{
    public abstract class AbstractBotService
    {
        protected readonly AbstractUserService _userService;
        protected AbstractBotService(AbstractUserService userService)
        {
            _userService = userService;
        }
        public abstract Task<ObjectResult> CreateBotAsync(int userId, CreateBotUserDto createBotUserDto);
        public abstract Task<ObjectResult> CustomizeBotAsync(int botId, BotUserProfileDto botUserProfileDto);
        public abstract Task<ObjectResult> DeleteBotAsync(int botId);
    }
}
