using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
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
using Microsoft.EntityFrameworkCore;

namespace _1_BusinessLayer.Concrete.Services
{
    public class UserService : AbstractUserService
    {
        public UserService(AbstractUserRepository userRepository, AbstractNotificationRepository notificationRepository, 
            AbstractActivityRepository activityRepository, AbstractBotRepository botRepository,
            AbstractUserPreferenceRepository preferenceRepository, AbstractEntryRepository entryRepository, 
            AbstractPostRepository postRepository, AbstractLikeRepository likeRepository, 
            AbstractFollowRepository followRepository, UserManager<User> userManager, 
            SignInManager<User> signInManager) 
            :base(userRepository, notificationRepository, activityRepository, botRepository, preferenceRepository, entryRepository, postRepository, likeRepository, followRepository, userManager, signInManager)
        {
        }

        public override async Task<IdentityResult> CreateProfileAsync(int userId, UserCreateProfileDto userCreateProfileDto)
        {           
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                if (user.IsProfileCreated == false)
                {
                    user = userCreateProfileDto.Update___UserCreateProfileDto_To_User(user);
                    user.IsProfileCreated = true;
                    var removeResult = await _userManager.RemoveFromRoleAsync(user, "TempUser");
                    if (!removeResult.Succeeded)
                        return IdentityResult.Failed(removeResult.Errors.ToArray());

                    var addResult = await _userManager.AddToRoleAsync(user, "StandardUser");
                    if (!addResult.Succeeded)
                        return IdentityResult.Failed(addResult.Errors.ToArray());

                    var stampResult = await _userManager.UpdateSecurityStampAsync(user);
                    if (!stampResult.Succeeded)
                        return IdentityResult.Failed(stampResult.Errors.ToArray());
                    await _signInManager.RefreshSignInAsync(user);
                    await _userRepository.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnauthorizedError("Profile already created initally"));
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

        public override async Task<IdentityResult> EditProfile(int userId, UserEditProfileDto userEditProfileDto)
        {
            var user = await _userRepository.GetUserModuleAsync(userId);
            if (user != null)
            {
                user = userEditProfileDto.Update___UserEditProfileDto_To_User(user);
                await _userRepository.SaveChangesAsync();
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<ObjectIdentityResult<List<BotActivityDto>>> LoadBotActivities(int userId, int startInterval, int endInterval)
        {
            var user = await _userRepository.GetUserModuleAsync(userId);
            List<BotActivityDto> botActivityDtos = new List<BotActivityDto>();
            if (user != null)
            {
                foreach (var bot in user.Bots)
                {
                    var botActivities = _activityRepository.GetBotActivityModulesForBotAsync(bot.BotId, startInterval , endInterval);
                    foreach (var activity in botActivities.Result)
                    {
                        botActivityDtos.Add(activity.BotActivity_To_BotActivityDto());
                    }
                }

                return ObjectIdentityResult<List<BotActivityDto>>.Succeded(botActivityDtos);      
            }
            return ObjectIdentityResult<List<BotActivityDto>>.Failed(null,new IdentityError[] {new NotFoundError("User not found") });
        }

        public override async Task<ObjectIdentityResult<dynamic>> GetBotPanel(int userId)
        {
            throw new NotImplementedException();

        }


        public override async Task<ObjectIdentityResult<List<NotificationDto>>> LoadNotifications(int userId, int startInterval, int endInterval)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            List<NotificationDto> notificationsDtos = new List<NotificationDto>();
            if (user != null)
            {
                List<Notification> notifications = await _notificationRepository.GetNotificationModulesForUser(userId, startInterval, endInterval);
                foreach (var notification in notifications)
                {
                    notificationsDtos.Add(notification.Notification_To_NotificationDto());
                }
                return ObjectIdentityResult<List<NotificationDto>>.Succeded(notificationsDtos);
            }
            return ObjectIdentityResult<List<NotificationDto>>.Failed(null, new IdentityError[] { new NotFoundError("User not found") });

        }


        public override async Task<ObjectIdentityResult<UserProfileDto>> GetUserProfile(int userId, int startInterval, int endInterval)
        {
           
            var user = await _userRepository.GetUserModuleAsync(userId);
            user.Posts = await _postRepository.GetPostModulesForUser(userId, startInterval, endInterval);
            user.Entries = await _entryRepository.GetEntryModulesForUserAsync(userId, startInterval, endInterval);
            user.Followers = await _followRepository.GetFollowModulesForUserAsFollowedAsync(userId, startInterval, endInterval);
            user.Followed = await _followRepository.GetFollowModulesForUserAsFollowerAsync(userId, startInterval, endInterval);

            if (user != null)
            {
                var userProfileDto = user.User_To_UserProfileDto();
                return ObjectIdentityResult<UserProfileDto>.Succeded(userProfileDto);
            }
            return ObjectIdentityResult<UserProfileDto>.Failed(null, new IdentityError[] { new NotFoundError("User not found") });

        }

        public override async Task<ObjectIdentityResult<List<EntryProfileDto>>> LoadProfileEntries(int userId, int startInterval, int endInterval)
        {
            var entries = await _entryRepository.GetEntryModulesForUserAsync(userId,startInterval,endInterval);
            List<EntryProfileDto> entryProfileDtos = new List<EntryProfileDto>();
            foreach (var entry in entries)
            {
                entryProfileDtos.Add(entry.Entry_To_EntryProfileDto());
            }
            return ObjectIdentityResult<List<EntryProfileDto>>.Succeded(entryProfileDtos);
        }

        public override async Task<ObjectIdentityResult<List<PostProfileDto>>> LoadProfilePosts(int userId, int startInterval, int endInterval)
        {
            var posts =  await _postRepository.GetPostModulesForUser(userId, startInterval, endInterval);
            List<PostProfileDto> postProfileDtos = new List<PostProfileDto>();
            foreach (var post in posts)
            {
                postProfileDtos.Add(post.Post_To_PostProfileDto());
            }
            return ObjectIdentityResult<List<PostProfileDto>>.Succeded(postProfileDtos);

        }

        public override async Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadProfileLikes(int userId, int startInterval, int endInterval)
        {
            var likes = await _likeRepository.GetLikeModulesForUser(userId, startInterval, endInterval);
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            foreach (var like in likes)
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return ObjectIdentityResult<List<MinimalLikeDto>>.Succeded(minimalLikeDtos);
        }
    }
}
