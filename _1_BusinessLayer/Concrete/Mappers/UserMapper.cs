using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos;
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
                Email = userRegisterDto.email,
                UserName = userRegisterDto.username,
            };
            return user;
        }

      
    }
}
