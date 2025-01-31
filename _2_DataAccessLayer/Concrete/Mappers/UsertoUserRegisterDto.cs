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
<<<<<<< HEAD:_2_DataAccessLayer/Concrete/Mappers/UserToUserDto.cs
        public static UserRegisterDto ToUserDto(this User user)
        {
            UserRegisterDto dto = new UserRegisterDto()
            {
                email = user.email,
                username = user.username,
                userId = user.userId,
                password = user.password,
=======
        public static UserRegisterDto ToRegisterUserDto(this User user)
        {
            UserRegisterDto dto = new UserRegisterDto()
            {
               email = user.Email,
               passwordHash = user.PasswordHash
>>>>>>> c4fa1372ff0b120a693caa0e06b6b496f66ec313:_2_DataAccessLayer/Concrete/Mappers/UsertoUserRegisterDto.cs
            };
            return dto;
        }
    }
}
