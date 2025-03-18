using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IBotService
    {
        Task<IdentityResult> CreateBot(CreateBotDto createBotDto);
        Task<IdentityResult> CustomizeBot(int botId, EditBotDto customizeBotDto);
        Task<IdentityResult> GetBotActivity(int botId);
        Task<ObjectIdentityResult<Bot>> GetBotProfile(int botId);
        Task<IdentityResult> DeployBot(int botId);
        Task<IdentityResult> DeleteBot(int userId, int botId);



    }
}
