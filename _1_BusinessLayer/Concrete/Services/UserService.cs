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
                    await _userRepository.UpdateAsync(user);
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


        public override async Task<ObjectIdentityResult<UserProfileDto>> GetUserProfile(int userId, int entryPerPagePreference = 10)
        {
            var listUser = await _userRepository.GetWithCustomSearchAsync(query => query
                .Where(user => user.Id == userId)
                .Select(user => new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsProfileCreated = user.IsProfileCreated,
                    DailyOperationCount = user.DailyOperationCount,
                    Entries = user.Entries.Take(entryPerPagePreference).Select(entry => new Entry
                    {
                        EntryId = entry.EntryId,
                        UserId = entry.UserId,
                        Context = entry.Context,
                        DateTime = entry.DateTime,
                        Likes = entry.Likes.Select(like => new Like
                        {
                            LikeId = like.LikeId,
                            UserId = like.UserId,
                            BotId = like.BotId,
                            EntryId = like.EntryId,
                            PostId = like.PostId,
                            DateTime = like.DateTime,
                            User = like.User, 
                            Bot = like.Bot 
                        }).ToList(),
                        User = user
                    }).ToList(),
                    Posts = user.Posts.Take(entryPerPagePreference).Select(post => new Post
                    {
                        PostId = post.PostId,
                        UserId = post.UserId,
                        Context = post.Context,
                        DateTime = post.DateTime,
                        Likes = post.Likes.Select(like => new Like
                        {
                            LikeId = like.LikeId,
                            UserId = like.UserId,
                            BotId = like.BotId,
                            EntryId = like.EntryId,
                            PostId = like.PostId,
                            DateTime = like.DateTime,
                            User = like.User,
                            Bot = like.Bot
                        }).ToList(),
                        User = user
                    }).ToList(),
                    Likes = user.Likes.Select(like => new Like
                    {
                        LikeId = like.LikeId,
                        UserId = like.UserId,
                        BotId = like.BotId,
                        EntryId = like.EntryId,
                        PostId = like.PostId,
                        DateTime = like.DateTime,
                        User = user
                    }).ToList(),
                    Followers = user.Followers.Select(follow => new Follow
                    {
                        FollowId = follow.FollowId,
                        DateTime = follow.DateTime,
                        UserFollowerId = follow.UserFollowerId,
                        UserFollowedId = follow.UserFollowedId,
                        BotFollowedId = follow.BotFollowedId,
                        BotFollowerId = follow.BotFollowerId,
                        UserFollower = follow.UserFollower,
                        UserFollowed = follow.UserFollowed,
                        BotFollower = follow.BotFollower,
                        BotFollowed = follow.BotFollowed
                    }).ToList(),
                    Followed = user.Followed.Select(follow => new Follow
                    {
                        FollowId = follow.FollowId,
                        DateTime = follow.DateTime,
                        UserFollowerId = follow.UserFollowerId,
                        UserFollowedId = follow.UserFollowedId,
                        BotFollowerId = follow.BotFollowerId,
                        BotFollowedId = follow.BotFollowedId,
                        UserFollower = follow.UserFollower,
                        UserFollowed = follow.UserFollowed,
                        BotFollower = follow.BotFollower,
                        BotFollowed = follow.BotFollowed
                    }).ToList()
                }));
            
            var user = listUser.FirstOrDefault();

            if (user != null)
            {
                var entryCount = await _userRepository.GetEntryCountOfUserAsync(userId);
                var postCount = await _userRepository.GetPostCountOfUserAsync(userId);
                var userProfileDto = user.User_To_UserProfileDto();
                userProfileDto.EntryCount = entryCount;
                userProfileDto.PostCount = postCount;
                return ObjectIdentityResult<UserProfileDto>.Succeded(userProfileDto);
            }
            return ObjectIdentityResult<UserProfileDto>.Failed(null, new IdentityError[] { new NotFoundError("User not found") });

        }

        public override async Task<ObjectIdentityResult<List<Entry>>> ReloadProfileEntries(int userId, int startInterval, int endInterval)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user != null)
            {
                List<Entry> entries = await _entryRepository.GetAllByUserIdWithIntervalsAsync(userId, startInterval, endInterval);
                return ObjectIdentityResult<List<Entry>>.Succeded(entries);
            }
            return ObjectIdentityResult<List<Entry>>.Failed(null, new IdentityError[] { new NotFoundError("User not found") });
        }

        public override async Task<ObjectIdentityResult<List<Post>>> ReloadProfilePosts(int userId, int startInterval, int endInterval)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                List<Post> posts = await _postRepository.GetAllByUserIdWithIntervalAsync(userId, startInterval, endInterval);
                return ObjectIdentityResult<List<Post>>.Succeded(posts);
            }
            return ObjectIdentityResult<List<Post>>.Failed(null, new IdentityError[] { new NotFoundError("User not found") });
        }
    }
}
