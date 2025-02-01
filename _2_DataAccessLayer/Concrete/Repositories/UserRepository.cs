using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete;


namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class UserRepository : AbstractUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override void Delete(User t)
        {
            _context.Users.Remove(t);
        }

        public override List<User> GetAll()
        {
            IQueryable<User> users = _context.Users;
            return users.ToList();
        }

        public override User GetById(int id)
        {
            User user = _context.Users.Find(id);
            return user;
        }

        public override User GetByName(string name)
        {
            User user = _context.Users.FirstOrDefault(user => user.UserName == name);
            return user;
        }

        public override void Insert(User t)
        {
            _context.Users.Add(t);
        }

        public override void Update(User t)
        {
            _context.Users.Attach(t);
            //make changes
        }
    }
}
