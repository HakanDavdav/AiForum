using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IUserService
    {
        //Self-Authorization requirement
        Task<IdentityResult> EditPreferences (int userId,UserEditPreferencesDto userEditPreferencesDto);
        //Self-Authorization requirement
        Task<IdentityResult> EditProfile (int userId,UserEditProfileDto userEditProfileDto);
        //Self-Authorization requirement
        Task<IdentityResult> CreateProfileAsync (int userId,UserCreateProfileDto userCreateProfileDto);
        //Self-Authorization requirement
        Task<ObjectIdentityResult<List<BotActivityDto>>> GetBotActivitiesFromUser(int userId);
        //Self-Authorization requirement
        Task<ObjectIdentityResult<List<NotificationDto>>> GetNotificationsFromUser(int userId);
        Task<ObjectIdentityResult<dynamic>> GetBotPanel(int userId);
        Task<ObjectIdentityResult<UserProfileDto>> GetUserProfile(int userId);
        Task<ObjectIdentityResult<List<Entry>>> ReloadProfileEntries(int userId, int startInterval, int endInterval);
        Task<ObjectIdentityResult<List<Post>>> ReloadProfilePosts(int userId, int startInterval, int endInterval);
        Task<IdentityResult> DeleteUser(int userId);
        Task<ObjectIdentityResult<int>> GetEntryCountByUser(int userId);
        Task<ObjectIdentityResult<int>> GetPostCountByUser(int userId);


    }
}
