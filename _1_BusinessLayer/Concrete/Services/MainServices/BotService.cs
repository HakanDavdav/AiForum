using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.MainAbstractServices;
using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Concrete.Services.MainServices
{
    public class BotService : AbstractBotService
    {
        public BotService(AbstractUserService userService) : base(userService)
        {
        }

        public override Task<ObjectResult> CreateBotAsync(int userId, CreateBotUserDto createBotUserDto)
        {
            throw new NotImplementedException();
        }

        public override Task<ObjectResult> CustomizeBotAsync(int botId, BotUserProfileDto botUserProfileDto)
        {
            throw new NotImplementedException();
        }

        public override Task<ObjectResult> DeleteBotAsync(int botId)
        {
            throw new NotImplementedException();
        }
    }
}
