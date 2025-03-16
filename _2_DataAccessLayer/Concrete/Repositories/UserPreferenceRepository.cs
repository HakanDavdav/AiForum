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

        public override async Task DeleteAsync(UserPreference t)
        {
            try
            {
                _context.UserPreferences.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL-related errors (Connection issues, timeout, syntax errors, etc.)
                Console.WriteLine($"SQL Error in DeleteAsync: {sqlEx.Message}");
                throw new Exception($"Database operation failed while deleting UserPreference: {sqlEx.Message}", sqlEx);
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Invalid operation error (Context is closed, object tracking issue, etc.)
                Console.WriteLine($"Invalid Operation Error in DeleteAsync: {invalidOpEx.Message}");
                throw new Exception($"Invalid operation while deleting UserPreference: {invalidOpEx.Message}", invalidOpEx);
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Database update error (FK, Unique Key violation, etc.)
                Console.WriteLine($"Database Update Error in DeleteAsync: {dbUpdateEx.Message}");
                throw new Exception($"Failed to delete UserPreference from the database: {dbUpdateEx.Message}", dbUpdateEx);
            }
        }

        public override async Task<UserPreference> GetByIdAsync(int id)
        {
            try
            {
                var userPreferences = await _context.UserPreferences.FirstOrDefaultAsync(userpreference => userpreference.UserPreferenceId == id);
#pragma warning disable CS8603 // Possible null reference return.
                return userPreferences;
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL-related errors (Connection issues, timeout, syntax errors, etc.)
                Console.WriteLine($"SQL Error in GetByIdAsync: {sqlEx.Message}");
                throw new Exception($"Database operation failed while retrieving UserPreference by ID: {sqlEx.Message}", sqlEx);
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Invalid operation error (Context is closed, object tracking issue, etc.)
                Console.WriteLine($"Invalid Operation Error in GetByIdAsync: {invalidOpEx.Message}");
                throw new Exception($"Invalid operation while retrieving UserPreference by ID: {invalidOpEx.Message}", invalidOpEx);
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Database update error (FK, Unique Key violation, etc.)
                Console.WriteLine($"Database Update Error in GetByIdAsync: {dbUpdateEx.Message}");
                throw new Exception($"Failed to retrieve UserPreference by ID from the database: {dbUpdateEx.Message}", dbUpdateEx);
            }
        }

        public override async Task<UserPreference> GetByUserIdAsync(int id)
        {
            try
            {
                var userPreference = await _context.UserPreferences.FirstOrDefaultAsync(userpreference => userpreference.UserId == id);
#pragma warning disable CS8603 // Possible null reference return.
                return userPreference;
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL-related errors (Connection issues, timeout, syntax errors, etc.)
                Console.WriteLine($"SQL Error in GetByUserIdAsync: {sqlEx.Message}");
                throw new Exception($"Database operation failed while retrieving UserPreference by UserId: {sqlEx.Message}", sqlEx);
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Invalid operation error (Context is closed, object tracking issue, etc.)
                Console.WriteLine($"Invalid Operation Error in GetByUserIdAsync: {invalidOpEx.Message}");
                throw new Exception($"Invalid operation while retrieving UserPreference by UserId: {invalidOpEx.Message}", invalidOpEx);
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Database update error (FK, Unique Key violation, etc.)
                Console.WriteLine($"Database Update Error in GetByUserIdAsync: {dbUpdateEx.Message}");
                throw new Exception($"Failed to retrieve UserPreference by UserId from the database: {dbUpdateEx.Message}", dbUpdateEx);
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
                // SQL-related errors (Connection issues, timeout, syntax errors, etc.)
                Console.WriteLine($"SQL Error in InsertAsync: {sqlEx.Message}");
                throw new Exception($"Database operation failed while inserting UserPreference: {sqlEx.Message}", sqlEx);
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Invalid operation error (Context is closed, object tracking issue, etc.)
                Console.WriteLine($"Invalid Operation Error in InsertAsync: {invalidOpEx.Message}");
                throw new Exception($"Invalid operation while inserting UserPreference: {invalidOpEx.Message}", invalidOpEx);
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Database update error (FK, Unique Key violation, etc.)
                Console.WriteLine($"Database Update Error in InsertAsync: {dbUpdateEx.Message}");
                throw new Exception($"Failed to insert UserPreference into the database: {dbUpdateEx.Message}", dbUpdateEx);
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
                // SQL-related errors (Connection issues, timeout, syntax errors, etc.)
                Console.WriteLine($"SQL Error in UpdateAsync: {sqlEx.Message}");
                throw new Exception($"Database operation failed while updating UserPreference: {sqlEx.Message}", sqlEx);
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Invalid operation error (Context is closed, object tracking issue, etc.)
                Console.WriteLine($"Invalid Operation Error in UpdateAsync: {invalidOpEx.Message}");
                throw new Exception($"Invalid operation while updating UserPreference: {invalidOpEx.Message}", invalidOpEx);
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Database update error (FK, Unique Key violation, etc.)
                Console.WriteLine($"Database Update Error in UpdateAsync: {dbUpdateEx.Message}");
                throw new Exception($"Failed to update UserPreference in the database: {dbUpdateEx.Message}", dbUpdateEx);
            }
        }
    }
}
