using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IBotService
    {
        //Self-Authorization requirement
        Task<IdentityResult> DeleteBot(int userId, int botId);
        //Self-Authorization requirement
        Task<IdentityResult> CreateBot(int userId, CreateBotDto createBotDto);
        //Self-Authorization requirement
        Task<IdentityResult> EditBot(int userId, EditBotDto editBotDto);
        //Self-Authorization requirement
        Task<IdentityResult> DeployBot(int userId, int botId);
        Task<ObjectIdentityResult<BotProfileDto>> GetBotProfile(int botId);
        Task<ObjectIdentityResult<BotProfileDto>> GetBotProfileFromBotActivity(int botActivityId);

    }
}
