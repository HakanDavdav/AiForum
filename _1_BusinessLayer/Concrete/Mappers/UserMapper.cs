﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Mappers
{
    public static class UserMapper
    {
        public static User UserRegisterToUser(this UserRegisterDto userRegisterDto)
        {
            var user = new User
            {
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.Username,
            };
            return user;
        }

        public static UserProfileDto UserToUserProfile(this User user)
        {
            var userProfileDto = new UserProfileDto
            {
                 Username = user.UserName,
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


    }
}
