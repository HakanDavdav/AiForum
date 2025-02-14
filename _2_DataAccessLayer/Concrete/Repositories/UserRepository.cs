using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;


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
            IQueryable<User> users = _context.Users.Include(user => user.posts).
                                                    ThenInclude(post => post.likes).
                                                    Include(user => user.entries).
                                                    ThenInclude(entries => entries.likes).
                                                    Include(user => user.likes).
                                                    Include(user => user.followers).
                                                    Include(user => user.followings);
            return users.ToList();
        }

        public override User GetById(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            User user = _context.Users.Include(user => user.posts).
                                       ThenInclude(post => post.likes).    
                                       Include(user => user.entries).
                                       ThenInclude(entries => entries.likes).
                                       Include(user => user.likes).
                                       Include(user => user.followers).
                                       Include(user => user.followings).
                                       FirstOrDefault(user => user.Id == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return user;
        }

        public override User GetByName(string name)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            User user = _context.Users.Include(user => user.posts).
                                       ThenInclude(post => post.likes).
                                       Include(user => user.entries).
                                       ThenInclude(entries => entries.likes).
                                       Include(user => user.likes).
                                       Include(user => user.followers).
                                       Include(user => user.followings).        
                                       FirstOrDefault(user => user.UserName == name);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
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
