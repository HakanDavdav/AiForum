using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class UserMappers
    {

        public static User UserRegisterDto_To_User(this UserRegisterDto userRegisterDto)
        {
            var user = new User
            {
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.Username,
            };
            return user;
        }

        public static MinimalUserDto User_To_MinimalUserDto(this User user)
        {
            var minimalUser = new MinimalUserDto
            {
                ImageUrl = user.ImageUrl,
                ProfileName = user.ProfileName,
                UserId = user.Id,
            };
            return minimalUser;
        }

        public static async Task<UserProfileDto> User_To_UserProfileDto(this User user, AbstractUserRepository _userRepository)
        {
            List<PostProfileDto> profilePosts = new List<PostProfileDto>();
            List<EntryProfileDto> profileEntries = new List<EntryProfileDto>();
            List<BotProfileDto> profileBots = new List<BotProfileDto>();
            List<FollowProfileDto> profileFollowed = new List<FollowProfileDto>();
            List<FollowProfileDto> profileFollowers = new List<FollowProfileDto>();

            foreach (var post in user.Posts)
            {
                profilePosts.Add(post.Post_To_PostProfileDto());
            }
            foreach (var entry in user.Entries)
            {
                profileEntries.Add(entry.Entry_To_EntryProfileDto());
            }
            foreach (var bot in user.Bots)
            {
                profileBots.Add(bot.Bot_To_BotProfileDto());
            }
            foreach (var follower in user.Followers)
            {
                var userFollower = await _userRepository.GetByIdAsync((int)follower.UserFolloweeId) ?? await _userRepository.GetByIdAsync((int)follower.BotFolloweeId);
                profileFollowers.Add(follower.Follow_To_FollowDto(userFollower.ImageUrl, userFollower.ProfileName, user.ImageUrl, user.ProfileName));
            }
            foreach (var followe_d in user.Followings)
            {
                var userFollowed = await _userRepository.GetByIdAsync((int)followe_d.UserFollowedId) ?? await _userRepository.GetByIdAsync((int)followe_d.BotFollowedId);
                profileFollowed.Add(followe_d.Follow_To_FollowDto(user.ImageUrl, user.ProfileName, userFollowed.ImageUrl, userFollowed.ProfileName));
            }


            var userProfileDto = new UserProfileDto
            {
                ImageUrl = user.ImageUrl,
                City = user.City,
                Entries = profileEntries,
                Posts = profilePosts,
                Bots = profileBots,
                Followers = profileFollowers,
                Followings = profileFollowed,
                Likes = user.Likes,
                ProfileName = user.ProfileName

            };
            return userProfileDto;
        }

        public static User Update___UserEditProfileDto_To_User(this UserEditProfileDto userEditProfileDto, User user)
        {
            user.ProfileName = userEditProfileDto.ProfileName;
            user.ImageUrl = userEditProfileDto.ImageUrl;
            user.City = userEditProfileDto.City;
            return user;
        }

        public static User Update___UserCreateProfileDto_To_User(this UserCreateProfileDto userCreateProfileDto, User user)
        {
            user.ProfileName = userCreateProfileDto.ProfileName;
            user.City = userCreateProfileDto.City;
            user.ImageUrl = userCreateProfileDto.ImageUrl;
            return user;
        }


        public static UserPreference Update___UserEditPreferencesDto_To_UserPreferences(this UserEditPreferencesDto userPreferencesDto, UserPreference userPreferences)
        {
            userPreferences.PostPerPage = userPreferencesDto.PostPerPage;
            userPreferences.EntryPerPage = userPreferencesDto.EntryPerPage;
            userPreferences.Notifications = userPreferencesDto.Notifications;
            userPreferences.Theme = userPreferencesDto.Theme;
            return userPreferences;
        }


    }
}
