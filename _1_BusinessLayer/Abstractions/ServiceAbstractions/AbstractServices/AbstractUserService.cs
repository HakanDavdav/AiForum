using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.BodyBuilders;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractUserService : IUserService
    {
        protected readonly AbstractGenericCommandHandler _commandHandler;
        protected readonly AbstractBotActivityQueryHandler _botActivityQueryHandler;
        protected readonly AbstractNotificationQueryHandler _notificationQueryHandler;
        protected readonly AbstractEntryQueryHandler _entryQueryHandler;
        protected readonly AbstractPostQueryHandler _postQueryHandler;
        protected readonly AbstractFollowQueryHandler _followQueryHandler;
        protected readonly AbstractLikeQueryHandler _likeQueryHandler;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;
        protected readonly NotificationActivityBodyBuilder _notificationActivityBodyBuilder;

        protected AbstractUserService(AbstractGenericCommandHandler genericBaseCommandHandler, AbstractBotActivityQueryHandler botActivityQueryHandler,
            AbstractNotificationQueryHandler notificationQueryHandler, AbstractEntryQueryHandler entryQueryHandler, AbstractPostQueryHandler postQueryHandler,
            AbstractFollowQueryHandler followQueryHandler)
        {
            
        }




        public abstract Task<IdentityResult> InitializeProfileAsync(int userId, UserCreateProfileDto userCreateProfileDto);
        public abstract Task<IdentityResult> DeleteUser(int userId);
        public abstract Task<IdentityResult> EditProfile(int userId, UserEditProfileDto userEditProfileDto);
        public abstract Task<ObjectIdentityResult<dynamic>> GetBotPanel(int userId);
        public abstract Task<ObjectIdentityResult<UserProfileDto>> GetUserProfile(int userId, ClaimsPrincipal claims);
        public abstract Task<ObjectIdentityResult<List<NotificationDto>>> LoadNotifications(int userId, int page);
        public abstract Task<ObjectIdentityResult<List<BotActivityDto>>> LoadBotActivities(int userId, int page);
        public abstract Task<ObjectIdentityResult<List<EntryProfileDto>>> LoadProfileEntries(int userId, ClaimsPrincipal claims, int page);
        public abstract Task<ObjectIdentityResult<List<PostProfileDto>>> LoadProfilePosts(int userId, ClaimsPrincipal claims, int page);
        public abstract Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadProfileLikes(int userId, int page);
        public abstract Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowers(int userId, int page);
        public abstract Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowed(int userId, int page);
        public abstract Task<ObjectIdentityResult<UserProfileSettingsDto>> GetUserProfileSettings(int userId);
        public abstract Task<ObjectIdentityResult<MinimalUserDto>> GetUserWithBotsTree(int userId);
    }
}
