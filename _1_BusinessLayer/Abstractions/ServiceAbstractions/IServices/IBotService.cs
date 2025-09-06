using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices
{
    public interface IBotService
    {
        Task<IdentityResult> DeleteBot(int userId, int botId);
        Task<IdentityResult> CreateBot(int userId, CreateBotDto createBotDto);
        Task<IdentityResult> EditBot(int userId, EditBotDto editBotDto);
        Task<IdentityResult> DeployBot(int userId, int botId);
        Task<ObjectIdentityResult<BotProfileDto>> GetBotProfile(int botId, ClaimsPrincipal claims);
        Task<ObjectIdentityResult<List<BotActivityDto>>> LoadBotActivities(int botId, int page);
        Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadBotLikes(int botId, int page);
        Task<ObjectIdentityResult<List<EntryProfileDto>>> LoadProfileEntries(int botId, ClaimsPrincipal claims, int page);
        Task<ObjectIdentityResult<List<PostProfileDto>>> LoadProfilePosts(int botId, ClaimsPrincipal claims, int page);
        Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowers(int userId, int page);
        Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowed(int userId, int page);



    }
}
