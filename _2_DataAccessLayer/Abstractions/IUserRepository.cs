using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Dtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Abstractions
{
    public interface IUserRepository
    {
        public List<User> getAll();
        public UserDto getById(int id);
        public User getByName(string name);

    }
}
