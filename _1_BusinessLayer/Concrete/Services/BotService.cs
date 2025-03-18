using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class BotService : AbstractBotService
    {
        public BotService(AbstractBotRepository botRepository, AbstractUserRepository userRepository) : base(botRepository, userRepository)
        {
        }

        public override async Task<IdentityResult> CreateBot(CreateBotDto createBotDto)
        {
            var bot = createBotDto.CreateBotDto_To_Bot();
            await _botRepository.InsertAsync(bot);
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> CustomizeBot(int botId, EditBotDto customizeBotDto)
        {
            var bot = await _botRepository.GetByIdAsync(botId);
            bot = customizeBotDto.Update___CustomizeBotDto_To_Bot(bot);
            await _botRepository.UpdateAsync(bot);
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeleteBot(int userId, int botId)
        {
            var bot = await _botRepository.GetByIdAsync(botId);
            if(bot.UserId == userId)
            {
                await _botRepository.DeleteAsync(bot);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("Unauthorized access"));
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
