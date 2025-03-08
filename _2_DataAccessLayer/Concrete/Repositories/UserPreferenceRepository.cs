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
            _context.UserPreferences.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<UserPreference>> GetAllAsync()
        {
            IQueryable<UserPreference> userPreferences = _context.UserPreferences;
            return await userPreferences.ToListAsync();
        }

        public override Task<List<UserPreference>> GetAllWithInfoAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<UserPreference> GetByIdAsync(int id)
        {
            var userPreferences = await _context.UserPreferences.FirstOrDefaultAsync(userpreference => userpreference.UserPreferenceId == id);
#pragma warning disable CS8603 // Possible null reference return.
            return userPreferences;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task<UserPreference> GetByIdWithInfoAsync(int id)
        {
            var userPreferences = await _context.UserPreferences.Include(userpreference => userpreference.User)
                                                                .FirstOrDefaultAsync(userpreference => userpreference.UserPreferenceId == id);
#pragma warning disable CS8603 // Possible null reference return.
            return userPreferences;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task<UserPreference> GetByUserIdAsync(int id)
        {
            var userPreference = await _context.UserPreferences.FirstOrDefaultAsync(userpreference => userpreference.UserId == id);
#pragma warning disable CS8603 // Possible null reference return.
            return userPreference;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task<UserPreference> GetByUserIdWithInfoAsync(int id)
        {
            var userPreference = await _context.UserPreferences.Include(userPreference => userPreference.User)
                                                               .FirstOrDefaultAsync(userpreference => userpreference.UserId == id);
#pragma warning disable CS8603 // Possible null reference return.
            return userPreference;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task InsertAsync(UserPreference t)
        {
            await _context.UserPreferences.AddAsync(t);
            await _context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(UserPreference t)
        {
            _context.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
