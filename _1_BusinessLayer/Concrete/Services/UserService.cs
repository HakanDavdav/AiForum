using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace _1_BusinessLayer.Concrete.Services
{
    public class UserService : AbstractUserService
    {
        public UserService(AbstractUserRepository userRepository, AbstractUserPreferenceRepository userPreferenceRepository, 
            AbstractNotificationRepository notificationRepository, AbstractActivityRepository activityRepository, AbstractBotRepository botRepository) 
            : base(userRepository, userPreferenceRepository, notificationRepository, activityRepository, botRepository)
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

        public override async Task<IdentityResult> DeleteUser(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> EditPreferences(int userId, UserEditPreferencesDto userEditPreferencesDto)
        {
            var preference = await _userPreferenceRepository.GetByUserIdAsync(userId);
            if (preference != null)
            {
                preference = userEditPreferencesDto.Update___UserEditPreferencesDto_To_UserPreferences(preference);
                await _userPreferenceRepository.UpdateAsync(preference);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User's preference not found"));
        }

        public override async Task<IdentityResult> EditProfile(int userId, UserEditProfileDto userEditProfileDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user = userEditProfileDto.Update___UserEditProfileDto_To_User(user);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<ObjectIdentityResult<List<BotActivityDto>>> GetBotActivitiesFromUser(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            List<BotActivityDto> botActivityDtos = new List<BotActivityDto>();
            if (user != null)
            {
                List<Bot> bots = await _botRepository.GetAllByUserIdAsync(userId);
                foreach (var bot in bots)
                {
                    List<BotActivity> botActivities = await _activityRepository.GetAllByBotIdAsync(bot.BotId);
                    foreach (var botActivity in botActivities)
                    {
                        botActivityDtos.Add(botActivity.BotActivity_To_BotActivityDto());
                    }

                }
                return ObjectIdentityResult<List<BotActivityDto>>.Succeded(botActivityDtos);      
            }
            return ObjectIdentityResult<List<BotActivityDto>>.Failed(null,new IdentityError[] {new NotFoundError("User not found") });
        }

        public override async Task<ObjectIdentityResult<List<NotificationDto>>> GetNotificationsFromUser(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            List<NotificationDto> notificationsDtos = new List<NotificationDto>();
            if (user != null)
            {
                List<Notification> notifications = await _notificationRepository.GetAllByUserIdAsync(userId);
                foreach (var notification in notifications)
                {
                    notificationsDtos.Add(notification.Notification_To_NotificationDto());
                }
                return ObjectIdentityResult<List<NotificationDto>>.Succeded(notificationsDtos);
            }
            return ObjectIdentityResult<List<NotificationDto>>.Failed(null, new IdentityError[] { new NotFoundError("User not found") });

        }

        public override async Task<ObjectIdentityResult<UserProfileDto>> GetUserProfile(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var userProfileDtO = user.User_To_UserProfileDto();
                return ObjectIdentityResult<UserProfileDto>.Succeded(userProfileDtO);
            }
            return ObjectIdentityResult<UserProfileDto>.Failed(null, new IdentityError[] { new NotFoundError("User not found") });

        }
    }
}
