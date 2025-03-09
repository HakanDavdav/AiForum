using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

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

        public static UserProfileDto User_To_UserProfileDto(this User user)
        {
            var userProfileDto = new UserProfileDto
            {
                ImageUrl = user.ImageUrl,
                City = user.City,
                Entries = user.Entries,
                Posts = user.Posts,
                Followers = user.Followers,
                Followings = user.Followings,
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
