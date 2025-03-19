using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Concrete.Services
{
    public class UserService : AbstractUserService
    {
        public UserService(AbstractUserRepository userRepository, AbstractUserPreferenceRepository userPreferenceRepository, 
            AbstractNotificationRepository notificationRepository, AbstractFollowRepository followRepository) 
            : base(userRepository, userPreferenceRepository, notificationRepository, followRepository)
        {
        }

        public override async Task<IdentityResult> CreateProfile(int userId, UserCreateProfileDto userCreateProfileDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user = userCreateProfileDto.Update___UserCreateProfileDto_To_User(user);
                await _userRepository.UpdateAsync(user);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }


        public override async Task<IdentityResult> EditPreferences(int userId, UserEditPreferencesDto userPreferencesDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var userPreference = await _userPreferenceRepository.GetByUserIdAsync(userId);
                userPreference = userPreferencesDto.Update___UserEditPreferencesDto_To_UserPreferences(userPreference);
                await _userPreferenceRepository.UpdateAsync(userPreference);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> EditProfile(int userId, UserEditProfileDto userEditProfileDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var updatedUser = userEditProfileDto.Update___UserEditProfileDto_To_User(user);
                await _userRepository.UpdateAsync(updatedUser);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));

        }


        public override async Task<ObjectIdentityResult<List<Notification>>> GetNotifications(int userId)
        {
            var notifications = await _notificationRepository.GetAllByUserIdWithInfoAsync(userId);
            return ObjectIdentityResult<List<Notification>>.Succeded(notifications);
            
        }

        public override async Task<ObjectIdentityResult<User>> GetUserProfile(int userId)
        {
            var user = await _userRepository.GetByIdWithInfoAsync(userId);
            if(user != null)
            {
                return ObjectIdentityResult<User>.Succeded(user);
            }
            return ObjectIdentityResult<User>.Failed(null,new IdentityError[] { new NotFoundError("User not found") });
        }

        public override async Task<IdentityResult> Follow(int userId, int followedUserId)
        {
           
        }

        public override async Task<IdentityResult> Unfollow(int userId, int followedUserId, int followId)
        {
          
        }
    }
}
