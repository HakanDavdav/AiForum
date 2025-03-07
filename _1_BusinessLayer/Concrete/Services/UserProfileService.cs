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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Concrete.Services
{
    public class UserProfileService : AbstractUserProfileService
    {
        public UserProfileService
            (AbstractUserRepository userRepository, AbstractUserPreferenceRepository userPreferenceRepository, 
            AbstractNotificationRepository notificationRepository) : base(userRepository, userPreferenceRepository, notificationRepository)
        {
        }

        public override async Task<IdentityResult> CreateProfile(int userId, UserCreateProfileDto userCreateProfileDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user = userCreateProfileDto.Update_UserCreateProfileDtoToUser(user);
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
                userPreference = userPreferencesDto.Update_UserEditPreferencesDtoToUserPreferences(userPreference);
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
                var updatedUser = userEditProfileDto.Update_UserEditProfileDtoToUser(user);
                await _userRepository.UpdateAsync(updatedUser);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));

        }

        public override async Task<ObjectIdentityResult<User>> GetUserProfile(int userId)
        {
            var userWithInfo = await _userRepository.GetByIdWithInfoAsync(userId);
            return ObjectIdentityResult<User>.Succededx(userWithInfo);
        }
    }
}
