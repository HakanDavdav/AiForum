using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices
{
    public interface IUserService
    {

        Task<IdentityResult> EditProfile (int userId,UserEditProfileDto userEditProfileDto);
        Task<IdentityResult> CreateProfileAsync (int userId,UserCreateProfileDto userCreateProfileDto);
        Task<ObjectIdentityResult<List<BotActivityDto>>> LoadBotActivities(int userId, int page);
        Task<ObjectIdentityResult<List<NotificationDto>>> LoadNotifications(int userId, int page);
        Task<ObjectIdentityResult<List<EntryProfileDto>>> LoadProfileEntries(int userId, ClaimsPrincipal claims, int page);
        Task<ObjectIdentityResult<List<PostProfileDto>>> LoadProfilePosts(int userId, ClaimsPrincipal claims, int page);
        Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowers(int userId, int page);
        Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowed(int userId, int page);
        Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadProfileLikes(int userId, int page);
        Task<ObjectIdentityResult<dynamic>> GetBotPanel(int userId);
        Task<ObjectIdentityResult<UserProfileDto>> GetUserProfile(int userId, ClaimsPrincipal claims);
        Task<ObjectIdentityResult<UserProfileSettingsDto>> GetUserProfileSettings(int userId);
        Task<IdentityResult> DeleteUser(int userId);


    }
}
