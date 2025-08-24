using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices
{
    public abstract class AbstractUserService : IUserService
    {
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractPostRepository _postRepository;
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractLikeRepository _likeRepository;
        protected readonly AbstractNotificationRepository _notificationRepository;
        protected readonly AbstractActivityRepository _activityRepository;
        protected readonly AbstractBotRepository _botRepository;
        protected readonly AbstractUserPreferenceRepository _preferenceRepository;
        protected readonly AbstractFollowRepository _followRepository;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;


        protected AbstractUserService
            (AbstractUserRepository userRepository, AbstractNotificationRepository notificationRepository, 
            AbstractActivityRepository activityRepository, AbstractBotRepository botRepository, AbstractUserPreferenceRepository preferenceRepository,
            AbstractEntryRepository entryRepository, AbstractPostRepository postRepository,AbstractLikeRepository likeRepository,
            AbstractFollowRepository followRepository,UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _activityRepository = activityRepository;
            _botRepository = botRepository;
            _preferenceRepository = preferenceRepository;
            _entryRepository = entryRepository;
            _postRepository = postRepository;
            _likeRepository = likeRepository;
            _followRepository = followRepository;
            _userManager = userManager;
        }

        public abstract Task<IdentityResult> CreateProfileAsync(int userId, UserCreateProfileDto userCreateProfileDto);
        public abstract Task<IdentityResult> DeleteUser(int userId);
        public abstract Task<IdentityResult> EditPreferences(int userId, UserEditPreferencesDto userEditPreferencesDto);
        public abstract Task<IdentityResult> EditProfile(int userId, UserEditProfileDto userEditProfileDto);
        public abstract Task<ObjectIdentityResult<dynamic>> GetBotPanel(int userId);
        public abstract Task<ObjectIdentityResult<UserProfileDto>> GetUserProfile(int userId, int startInterval, int endInterval);
        public abstract Task<ObjectIdentityResult<List<NotificationDto>>> LoadNotifications(int userId, int startInterval, int endInterval);
        public abstract Task<ObjectIdentityResult<List<BotActivityDto>>> LoadBotActivities(int userId, int startInterval, int endInterval);
        public abstract Task<ObjectIdentityResult<List<EntryProfileDto>>> LoadProfileEntries(int userId, int startInterval, int endInterval);
        public abstract Task<ObjectIdentityResult<List<PostProfileDto>>> LoadProfilePosts(int userId, int startInterval, int endInterval);
    }
}
