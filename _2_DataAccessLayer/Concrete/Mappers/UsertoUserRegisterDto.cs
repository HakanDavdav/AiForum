using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Dtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Concrete.Mappers
{
    public static class UserToUserRegisterDto
    {
        public static UserRegisterDto ToRegisterUserDto(this User user)
        {
            UserRegisterDto dto = new UserRegisterDto()
            {
               email = user.Email,
               passwordHash = user.PasswordHash
            };
            return dto;
        }
    }
}
