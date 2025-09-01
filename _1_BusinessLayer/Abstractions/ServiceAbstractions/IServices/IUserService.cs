using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
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

        //Self-Authorization requirement
        Task<IdentityResult> EditProfile (int userId,UserEditProfileDto userEditProfileDto);
        //Self-Authorization requirement
        Task<IdentityResult> CreateProfileAsync (int userId,UserCreateProfileDto userCreateProfileDto);
        //Self-Authorization requirement
        Task<ObjectIdentityResult<List<BotActivityDto>>> LoadBotActivities(int userId, int startInterval, int endInterval);
        //Self-Authorization requirement
        Task<ObjectIdentityResult<List<NotificationDto>>> LoadNotifications(int userId, int startInterval, int endInterval);
        Task<ObjectIdentityResult<List<EntryProfileDto>>> LoadProfileEntries(int userId, int startInterval, int endInterval);
        Task<ObjectIdentityResult<List<PostProfileDto>>> LoadProfilePosts(int userId, int startInterval, int endInterval);
        Task<ObjectIdentityResult<dynamic>> GetBotPanel(int userId);
        Task<ObjectIdentityResult<UserProfileDto>> GetUserProfile(int userId, int startInterval, int endInterval);
        Task<IdentityResult> DeleteUser(int userId);


    }
}
