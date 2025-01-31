using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Dtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Concrete.Mappers
{
    public static class UserToUserDto
    {
        public static UserRegisterDto ToUserDto(this User user)
        {
            UserRegisterDto dto = new UserRegisterDto()
            {
                email = user.email,
                username = user.username,
                userId = user.userId,
                password = user.password,
            };
            return dto;
        }
    }
}
