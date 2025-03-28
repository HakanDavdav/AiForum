using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class UserPreferenceRepository : AbstractUserPreferenceRepository
    {
        public UserPreferenceRepository(ApplicationDbContext context, ILogger<UserPreference> logger) : base(context, logger)
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
                _logger.LogError(sqlEx, "SQL Error in CheckEntity with UserPreferenceId {UserPreferenceId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in CheckEntity with UserPreferenceId {UserPreferenceId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in CheckEntity with UserPreferenceId {UserPreferenceId}", id);
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
                _logger.LogError(sqlEx, "SQL Error in DeleteAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in DeleteAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in DeleteAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
                throw;
            }
        }

        public override Task<List<UserPreference>> GetAllWithCustomSearch(Func<IQueryable<UserPreference>, IQueryable<UserPreference>> queryModifier)
        {
            throw new NotImplementedException();
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
                _logger.LogError(sqlEx, "SQL Error in GetByIdAsync with UserPreferenceId {UserPreferenceId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByIdAsync with UserPreferenceId {UserPreferenceId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByIdAsync with UserPreferenceId {UserPreferenceId}", id);
                throw;
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
                _logger.LogError(sqlEx, "SQL Error in GetByUserIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByUserIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByUserIdAsync with UserId {UserId}", id);
                throw;
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
                _logger.LogError(sqlEx, "SQL Error in InsertAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in InsertAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in InsertAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
                throw;
            }
        }

        public override async Task UpdateAsync(UserPreference t)
        {
            try
            {
                _context.UserPreferences.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in UpdateAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in UpdateAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in UpdateAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
                throw;
            }
        }
    }
}
