using System;
using System.Collections.Generic;
using System.Linq;
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
                return await _context.UserPreferences.AnyAsync(up => up.UserPreferenceId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckEntity with UserPreferenceId {UserPreferenceId}", id);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
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
                return await _context.UserPreferences.FirstOrDefaultAsync(up => up.UserPreferenceId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with UserPreferenceId {UserPreferenceId}", id);
                throw;
            }
        }

        public override async Task<UserPreference> GetByUserIdAsync(int id)
        {
            try
            {
                return await _context.UserPreferences.FirstOrDefaultAsync(up => up.UserId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByUserIdAsync with UserId {UserId}", id);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InsertAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for UserPreferenceId {UserPreferenceId}", t.UserPreferenceId);
                throw;
            }
        }
    }
}
