using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
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
        public static MinimalUserDto User_To_MinimalUserDto(this User user)
        {
            return new MinimalUserDto
            {
                ImageUrl = user.ImageUrl,
                ProfileName = user.ProfileName,
                UserId = user.Id,
            };
        }

        public static UserProfileDto User_To_UserProfileDto(this User user)
        {
            List<MinimalBotDto> minimalBotDtos = new List<MinimalBotDto>();
            List<PostProfileDto> postProfileDtos = new List<PostProfileDto>();
            List<EntryProfileDto> entryProfileDtos = new List<EntryProfileDto>();
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            List<FollowProfileDto> followerDtos = new List<FollowProfileDto>();
            List<FollowProfileDto> followedDtos = new List<FollowProfileDto>();
            foreach (var bot in user.Bots ?? new List<Bot>())
            {
                minimalBotDtos.Add(bot.Bot_To_MinimalBotDto());
            }
            foreach (var post in user.Posts ?? new List<Post>())
            {
                postProfileDtos.Add(post.Post_To_PostProfileDto());
            }
            foreach (var entry in user.Entries ?? new List<Entry>())
            {
                entryProfileDtos.Add(entry.Entry_To_EntryProfileDto());
            }
            foreach (var like in user.Likes ?? new List<Like>())
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            foreach (var follower in user.Followers ?? new List<Follow>())
            {
                followerDtos.Add(follower.Follow_To_FollowProfileDto());
            }
            foreach (var followed in user.Followed ?? new List<Follow>())
            {
                followedDtos.Add(followed.Follow_To_FollowProfileDto());
            }
            return new UserProfileDto
            {
                City = user.City,
                Date = user.DateTime,
                ProfileName = user.ProfileName,
                ImageUrl = user.ImageUrl,
                UserId = user.Id,
                Bots = minimalBotDtos,
                Entries = entryProfileDtos,
                Posts = postProfileDtos,
                Likes = minimalLikeDtos,
                Followers = followedDtos,
                Followed = followedDtos,
            };
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

        public static User UserRegisterDto_To_User(this UserRegisterDto userRegisterDto)
        {
            return new User
            {
                UserName = userRegisterDto.Username,
                Email = userRegisterDto.Email              
            };
        }


    }
}
