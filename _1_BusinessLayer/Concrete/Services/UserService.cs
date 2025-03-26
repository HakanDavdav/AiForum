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

namespace _1_BusinessLayer.Concrete.Services
{
    public class UserService : AbstractUserService
    {
        public UserService(AbstractUserRepository userRepository, AbstractNotificationRepository notificationRepository,
            AbstractActivityRepository activityRepository, AbstractBotRepository botRepository, 
            AbstractUserPreferenceRepository preferenceRepository, AbstractEntryRepository entryRepository, 
            AbstractPostRepository postRepository, AbstractLikeRepository likeRepository, AbstractFollowRepository followRepository) 
            : base(userRepository, notificationRepository, activityRepository, botRepository, preferenceRepository, entryRepository, postRepository, likeRepository, followRepository)
        {
        }

        public override async Task<IdentityResult> CreateProfileAsync(int userId, UserCreateProfileDto userCreateProfileDto)
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
            var preference = await _preferenceRepository.GetByUserIdAsync(userId);
            if (preference != null)
            {
                preference = userEditPreferencesDto.Update___UserEditPreferencesDto_To_UserPreferences(preference);
                await _preferenceRepository.UpdateAsync(preference);
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

        public override async Task<ObjectIdentityResult<dynamic>> GetBotPanel(int userId)
        {
            Dictionary<Bot, TimeSpan> myDict = new Dictionary<Bot, TimeSpan>();

            var user = await _userRepository.GetByIdAsync(userId);
            List<Bot> bots = await _botRepository.GetAllByUserIdAsync(userId);
            DateTime currentTime = DateTime.Now;

            foreach (var bot in bots)
            {
                // Calculate the remaining time
                var remainingTime = currentTime - bot.DeployDateTime;

                // If the remaining time is negative (i.e., the time has passed), skip the bot
                if (remainingTime.TotalSeconds <= 0)
                {
                    continue; // Skip this bot, no need to add to the dictionary
                }

                // Add the bot and remaining time to the dictionary
                myDict.Add(bot, remainingTime);
            }

            if (user != null)
            {
                dynamic result = new
                {
                    Bots = bots,
                    UserDailyOperationCount = user.DailyOperationCount,
                    NumberOfBots = bots.Count,
                    BotsWithRemainingTimes = myDict
                };
                return ObjectIdentityResult<dynamic>.Succeded(result);
            }
            return ObjectIdentityResult<dynamic>.Failed(null, new IdentityError[] {new NotFoundError("User not found") });

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
                List<Entry> entries = await _entryRepository.GetAllByUserIdAsync(userId);
                List<Post> posts = await _postRepository.GetAllByUserIdAsync(userId);
                List<Like> likes = await _likeRepository.GetAllByUserIdAsync(userId);
                List<Follow> followers = await _followRepository.GetAllByUserIdAsFollowedWithInfoAsync(userId);
                List<Follow> followed = await _followRepository.GetAllByUserIdAsFollowerWithInfoAsync(userId);

                foreach (var entry in entries)
                {
                    entry.User = await _userRepository.GetByIdAsync((int)entry.UserId);
                    entry.Likes = await _likeRepository.GetAllByEntryIdAsync((int)entry.EntryId);
                    foreach (var entryLike in entry.Likes)
                    {
                        entryLike.User = await _userRepository.GetByIdAsync((int)entryLike.UserId);
                        entryLike.Bot = await _botRepository.GetByIdAsync(((int)entryLike.BotId));
                    }
                }

                foreach (var post in posts)
                {
                    post.User = await _userRepository.GetByIdAsync((int)post.UserId);
                    post.Likes = await _likeRepository.GetAllByPostIdAsync(post.PostId);
                    foreach (var postLike in post.Likes)
                    {
                        postLike.User = await _userRepository.GetByIdAsync((int)postLike.UserId);
                        postLike.Bot = await _botRepository.GetByIdAsync(((int)(postLike.BotId)));
                    }
                }

                foreach (var like in likes)
                {
                    like.User = await _userRepository.GetByIdAsync(userId);      
                }

                user.Entries = entries;
                user.Posts = posts;
                user.Likes = likes;
                user.Followers = followers;
                user.Followed = followed;
                var userProfileDto = user.User_To_UserProfileDto();
                return ObjectIdentityResult<UserProfileDto>.Succeded(userProfileDto);
            }
            return ObjectIdentityResult<UserProfileDto>.Failed(null, new IdentityError[] { new NotFoundError("User not found") });

        }
    }
}
