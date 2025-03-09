using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class UserRepository : AbstractUserRepository
    {
        public UserRepository(ApplicationDbContext context, UserManager<User> userManager) : base(context, userManager)
        {
        }

        public override async Task DeleteAsync(User t)
        {
            _context.Users.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<User>> GetAllAsync()
        {
            IQueryable<User> users = _context.Users;                                                     
            return await users.ToListAsync();
        }

        public override async Task<List<User>> GetAllWithInfoAsync()
        {
            IQueryable<User> users = _context.Users.Include(user => user.Posts).
                                                    ThenInclude(post => post.Likes).
                                                    Include(user => user.Entries).
                                                    ThenInclude(entries => entries.Likes).
                                                    Include(user => user.Followers).
                                                    Include(user => user.Followings).
                                                    Include(user => user.Bots).
                                                    Include(user => user.UserPreference);                                                
            return await users.ToListAsync();
        }

        public override async Task<User> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            User user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return user;
        }

        public override async Task<User> GetByIdWithInfoAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            User user = await _context.Users.Include(user => user.Posts).
                                                    ThenInclude(post => post.Likes).
                                                    Include(user => user.Entries).
                                                    ThenInclude(entries => entries.Likes).
                                                    Include(user => user.Followers).
                                                    Include(user => user.Followings).
                                                    Include(user => user.Bots).
                                                    Include(user => user.UserPreference).
                                                    FirstOrDefaultAsync(user => user.Id == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return user;
        }

        public override async Task<User> GetByUsernameAsync(string name)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            User user = await _context.Users.FirstOrDefaultAsync(user => user.UserName == name);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return user;
        }

        public override async Task<User> GetByEmailAsync(string email)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            User user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return user;
        }


        public override async Task InsertAsync(User t)
        {
            await _context.Users.AddAsync(t);
            await _context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(User t)
        {
            _context.Update(t);
            await _context.SaveChangesAsync();
        }

        public override Task<User> GetByProfileName(string query)
        {
            throw new NotImplementedException();
        }

        //Identity does not have any roles in it's own user table
        public override async Task<List<string>> GetUserRolesAsync(User user)
        {
            List<string> roles = (List<string>)await _userManager.GetRolesAsync(user);
            return roles;
        }
    }
}
