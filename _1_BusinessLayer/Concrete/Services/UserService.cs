using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Dtos;

namespace _1_BusinessLayer.Concrete.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            {
                _userRepository = userRepository;
            }
        }

        public UserDto GetUserByID(int id)
        {
            return _userRepository.getById(id);
        }
    }
}
