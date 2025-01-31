using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Mappers;
using _2_DataAccessLayer.Concrete.Dtos;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<User> getAll()
        {
            throw new NotImplementedException();
        }

        public UserRegisterDto getById(int id)
        {
            UserRegisterDto user = _context.users.FirstOrDefault(p => p.userId == id).ToUserDto();
            return user;
        }

        public User getByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
