using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class UserPreferenceRepository : AbstractUserPreferenceRepository
    {
        public UserPreferenceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.UserPreferences.AnyAsync(userPreference => userPreference.UserPreferenceId == id);
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
        public override async Task DeleteAsync(UserPreference t)
        {
            try
            {
                _context.UserPreferences.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in DeleteAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in DeleteAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in DeleteAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task<UserPreference> GetByIdAsync(int id)
        {
            try
            {
                var userPreference = await _context.UserPreferences.FirstOrDefaultAsync(userpreference => userpreference.UserPreferenceId == id);
                return userPreference;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetByIdAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetByIdAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetByIdAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task<UserPreference> GetByUserIdAsync(int id)
        {
            try
            {
                var userPreference = await _context.UserPreferences.FirstOrDefaultAsync(userpreference => userpreference.UserId == id);
                return userPreference;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetByUserIdAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetByUserIdAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetByUserIdAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task InsertAsync(UserPreference t)
        {
            try
            {
                await _context.UserPreferences.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in InsertAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in InsertAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in InsertAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task UpdateAsync(UserPreference t)
        {
            try
            {
                _context.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in UpdateAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in UpdateAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in UpdateAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
            }
        }
    }
}
