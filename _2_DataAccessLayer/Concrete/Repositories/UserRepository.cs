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

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Users.AnyAsync(user => user.Id == id);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in CheckEntity: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in CheckEntity: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in CheckEntity: {dbUpdateEx.Message}");
                throw;
            }
        }

        public override async Task DeleteAsync(User t)
        {
            try
            {
                _context.Users.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine($"SQL Error in DeleteAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid Operation Error in DeleteAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database Update Error in DeleteAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            try
            {
#pragma warning disable CS8603 // Possible null reference return.
                return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetByIdAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid Operation Error in GetByIdAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database Update Error in GetByIdAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task<User> GetByUsernameAsync(string name)
        {
            try
            {
#pragma warning disable CS8603 // Possible null reference return.
                return await _context.Users.FirstOrDefaultAsync(user => user.UserName == name);
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetByUsernameAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid Operation Error in GetByUsernameAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database Update Error in GetByUsernameAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task<User> GetByEmailAsync(string email)
        {
            try
            {
#pragma warning disable CS8603 // Possible null reference return.
                return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetByEmailAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid Operation Error in GetByEmailAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database Update Error in GetByEmailAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task InsertAsync(User t)
        {
            try
            {
                await _context.Users.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine($"SQL Error in InsertAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid Operation Error in InsertAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database Update Error in InsertAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task UpdateAsync(User t)
        {
            try
            {
                _context.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine($"SQL Error in UpdateAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid Operation Error in UpdateAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database Update Error in UpdateAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task<User> GetByProfileNameAsync(string profileName)
        {
            try
            {
#pragma warning disable CS8603 // Possible null reference return.
                return await _context.Users.FirstOrDefaultAsync(user => user.ProfileName == profileName);
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetByProfileNameAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid Operation Error in GetByProfileNameAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database Update Error in GetByProfileNameAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task<User> GetByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
#pragma warning disable CS8603 // Possible null reference return.
                return await _context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetByPhoneNumberAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid Operation Error in GetByPhoneNumberAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database Update Error in GetByPhoneNumberAsync: {ex.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task<List<User>> GetRandomUsers(int number)
        {
            try
            {
                IQueryable<User> users = _context.Users.OrderBy(user => Guid.NewGuid()).Take(number);
                return await users.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetRandomUsers: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid Operation Error in GetRandomUsers: {ex.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database Update Error in GetRandomUsers: {ex.Message}");
                throw; // Rethrow the caught exception
            }
        }
    }
}
