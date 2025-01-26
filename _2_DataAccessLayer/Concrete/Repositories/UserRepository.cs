using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

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

        public User getById(int id)
        {
            throw new NotImplementedException();
        }

        public User getByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
