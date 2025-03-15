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



        public override async Task<User> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            User user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
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

        public override async Task<User> GetByProfileNameAsync(string profileName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user =>user.ProfileName == profileName);
            return user;
        }

        public override async Task<User> GetByPhoneNumberAsync(string phoneNumber)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
            return user;
        }

        public override async Task<List<User>> GetRandomUsers(int number)
        {
            IQueryable<User> users = _context.Users.OrderBy(user => Guid.NewGuid()).Take(number);
            return await users.ToListAsync();
        }
    }
}
