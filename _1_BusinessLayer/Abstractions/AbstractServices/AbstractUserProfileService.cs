using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_BusinessLayer.Abstractions.AbstractServices
{
    public abstract class AbstractUserProfileService: IUserProfileService
    {
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractUserPreferenceRepository _userPreferenceRepository;
        protected readonly AbstractNotificationRepository _notificationRepository;
        protected AbstractUserProfileService
            (AbstractUserRepository userRepository,AbstractUserPreferenceRepository userPreferenceRepository,AbstractNotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _userPreferenceRepository = userPreferenceRepository;
        }

        public abstract Task<IdentityResult> CreateProfile(int userId, UserCreateProfileDto userCreateProfileDto);
        public abstract Task<IdentityResult> EditPreferences(int userId, UserEditPreferencesDto userEditPreferencesDto);
        public abstract Task<IdentityResult> EditProfile(int userId, UserEditProfileDto userEditProfileDto);
        public abstract Task<ObjectIdentityResult<List<Notification>>> GetNotifications(int userId);
        public abstract Task<ObjectIdentityResult<User>> GetUserProfile(int userId);
    }
}
