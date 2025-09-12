using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.BodyBuilders;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace _1_BusinessLayer.Concrete.Services
{
    public class UserService : AbstractUserService
    {
        public UserService(AbstractUserRepository userRepository, AbstractNotificationRepository notificationRepository, AbstractActivityRepository activityRepository,
            AbstractBotRepository botRepository, AbstractUserPreferenceRepository preferenceRepository, AbstractEntryRepository entryRepository,
            AbstractPostRepository postRepository, AbstractLikeRepository likeRepository, AbstractFollowRepository followRepository,
            UserManager<User> userManager, SignInManager<User> signInManager, NotificationActivityBodyBuilder notificationActivityBodyBuilder) :
            base(userRepository, notificationRepository, activityRepository, botRepository, preferenceRepository,
                entryRepository, postRepository, likeRepository, followRepository, userManager, signInManager, notificationActivityBodyBuilder)
        {
        }

        public override async Task<IdentityResult> InitializeProfileAsync(int userId, UserCreateProfileDto userCreateProfileDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return
                    IdentityResult.Failed(new NotFoundError("User not found"));
            if (user.IsProfileCreated == true)
                return IdentityResult.Failed(new UnauthorizedError("Profile already created initally"));

            user = userCreateProfileDto.Update___UserCreateProfileDto_To_User(user);
            user.IsProfileCreated = true;
            var removeResult = await _userManager.RemoveFromRoleAsync(user, "TempUser");
            if (!removeResult.Succeeded)
                return removeResult;

            var addResult = await _userManager.AddToRoleAsync(user, "StandardUser");
            if (!addResult.Succeeded)
                return addResult;

            var stampResult = await _userManager.UpdateSecurityStampAsync(user);
            if (!stampResult.Succeeded)
                return stampResult;
            await _signInManager.RefreshSignInAsync(user);
            await _userRepository.SaveChangesAsync();
            return IdentityResult.Success;

        }

        public override async Task<IdentityResult> DeleteUser(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) 
                return IdentityResult.Failed(new NotFoundError("User not found"));
            await _userRepository.DeleteAsync(user);
            return IdentityResult.Success;

        }

        //cannot use for deletion of bots.
        public override async Task<IdentityResult> EditProfile(int userId, UserEditProfileDto userEditProfileDto)
        {
            var user = await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(user => user.Id == userId).Include(user => user.UserPreference).Include(user => user.Bots));
            if (user == null) 
                return IdentityResult.Failed(new NotFoundError("User not found"));
            user = userEditProfileDto.Update___UserEditProfileDto_To_User(user);
            var oldClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in oldClaims)
            {
                await _userManager.RemoveClaimAsync(user, claim);
            }
            var claims = new List<Claim>
                  {
                     new Claim("Theme",user.UserPreference.Theme),
                     new Claim("PostPerPage", user.UserPreference.PostPerPage.ToString()),
                     new Claim("EntryPerPage", user.UserPreference.EntryPerPage.ToString())
                  };
            await _userManager.AddClaimsAsync(user, claims);
            await _signInManager.RefreshSignInAsync(user);
            await _userRepository.SaveChangesAsync();
            return IdentityResult.Success;

        }

        public override async Task<ObjectIdentityResult<List<BotActivityDto>>> LoadBotActivities(int userId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var botActivities = await _activityRepository.GetBotActivityModulesForUserAsync(userId, startInterval, endInterval);
            var botActivityDtos = new List<BotActivityDto>();
            foreach (var activity in botActivities)
            {
                var (title, body) = _notificationActivityBodyBuilder.BuildAppBotActivityContent(activity);
                botActivityDtos.Add(activity.BotActivity_To_BotActivityDto(body, title));
            }
            return ObjectIdentityResult<List<BotActivityDto>>.Succeded(botActivityDtos);
        }

        public override async Task<ObjectIdentityResult<dynamic>> GetBotPanel(int userId)
        {
            throw new NotImplementedException();

        }


        public override async Task<ObjectIdentityResult<UserProfileDto>> GetUserProfile(int userId, ClaimsPrincipal claims)
        {
            var startInterval = 0;
            var endInterval = claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10;
            var user = await _userRepository.GetUserModuleAsync(userId);
            if (user == null)
                return ObjectIdentityResult<UserProfileDto>.Failed(null, new IdentityError[] { new NotFoundError("User not found") });
            user.Posts = await _postRepository.GetPostModulesForUser(userId, startInterval, endInterval);
            user.Entries = await _entryRepository.GetEntryModulesForUserAsync(userId, startInterval, endInterval);
            user.Followers = await _followRepository.GetFollowModulesForUserAsFollowedAsync(userId, startInterval, endInterval);
            user.Followed = await _followRepository.GetFollowModulesForUserAsFollowerAsync(userId, startInterval, endInterval);
            var userProfileDto = user.User_To_UserProfileDto();
            return ObjectIdentityResult<UserProfileDto>.Succeded(userProfileDto);

        }

        public override async Task<ObjectIdentityResult<List<EntryProfileDto>>> LoadProfileEntries(int userId, ClaimsPrincipal claims, int page)
        {
            var startInterval = (page - 1) * (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var endInterval = startInterval + (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var entries = await _entryRepository.GetEntryModulesForUserAsync(userId, startInterval, endInterval);
            List<EntryProfileDto> entryProfileDtos = new List<EntryProfileDto>();
            foreach (var entry in entries)
            {
                entryProfileDtos.Add(entry.Entry_To_EntryProfileDto());
            }
            return ObjectIdentityResult<List<EntryProfileDto>>.Succeded(entryProfileDtos);
        }

        public override async Task<ObjectIdentityResult<List<PostProfileDto>>> LoadProfilePosts(int userId, ClaimsPrincipal claims, int page)
        {
            var startInterval = (page - 1) * (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var endInterval = startInterval + (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var posts = await _postRepository.GetPostModulesForUser(userId, startInterval, endInterval);
            List<PostProfileDto> postProfileDtos = new List<PostProfileDto>();
            foreach (var post in posts)
            {
                postProfileDtos.Add(post.Post_To_PostProfileDto());
            }
            return ObjectIdentityResult<List<PostProfileDto>>.Succeded(postProfileDtos);

        }

        public override async Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadProfileLikes(int userId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var likes = await _likeRepository.GetLikeModulesForUser(userId, startInterval, endInterval);
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            foreach (var like in likes)
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return ObjectIdentityResult<List<MinimalLikeDto>>.Succeded(minimalLikeDtos);
        }

        public override async Task<ObjectIdentityResult<List<NotificationDto>>> LoadNotifications(int userId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var user = await _userRepository.GetByIdAsync(endInterval);
            List<NotificationDto> notificationsDtos = new List<NotificationDto>();
            List<Notification> notifications = await _notificationRepository.GetNotificationModulesForUser(userId, startInterval, endInterval);
            foreach (var notification in notifications)
            {
                var (title, body) = _notificationActivityBodyBuilder.BuildAppNotificationContent(notification);
                notificationsDtos.Add(notification.Notification_To_NotificationDto(body, title));
            }
            return ObjectIdentityResult<List<NotificationDto>>.Succeded(notificationsDtos);
        }

        public override async Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowers(int userId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            List<FollowProfileDto> followProfileDtos = new List<FollowProfileDto>();
            var followers = await _followRepository.GetFollowModulesForUserAsFollowedAsync(userId, startInterval, endInterval);
            foreach (var follow in followers)
            {
                followProfileDtos.Add(follow.Follow_To_FollowProfileDto());
            }
            return ObjectIdentityResult<List<FollowProfileDto>>.Succeded(followProfileDtos);

        }

        public override async Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowed(int userId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            List<FollowProfileDto> followProfileDtos = new List<FollowProfileDto>();
            var followers = await _followRepository.GetFollowModulesForUserAsFollowerAsync(userId, startInterval, endInterval);
            foreach (var follow in followers)
            {
                followProfileDtos.Add(follow.Follow_To_FollowProfileDto());
            }
            return ObjectIdentityResult<List<FollowProfileDto>>.Succeded(followProfileDtos);
        }

        public override async Task<ObjectIdentityResult<UserProfileSettingsDto>> GetUserProfileSettings(int userId)
        {
            var user = await _userRepository.GetUserModuleAsync(userId);
            var userProfileSettingsDto = user.User_To_UserProfileSettingsDto();
            return ObjectIdentityResult<UserProfileSettingsDto>.Succeded(userProfileSettingsDto);
        }

        public override async Task<ObjectIdentityResult<MinimalUserDto>> GetUserWithBotsTree(int userId)
        {
            var user = await _userRepository.GetUserWithBotTreeAsync(userId);
            var minimalUserDto = user.UserWithBotTree_To_MinimalVersion();
            return ObjectIdentityResult<MinimalUserDto>.Succeded(minimalUserDto);
        }
    }
}
