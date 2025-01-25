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
        public static UserDto ToUserDto(this User user)
        {
            UserDto dto = new UserDto()
            {
                email = user.email,
                name = user.name,
                userId = user.userId,
            };
            return dto;
        }
    }
}
