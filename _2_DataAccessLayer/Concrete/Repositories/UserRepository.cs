using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class UserRepository : AbstractUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger<User> logger) : base(context, logger)
        {
        }

        public override async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public override async Task DeleteAsync(User t)
        {
            try
            {
                _context.Users.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for UserId {UserId}", t.Id);
                throw;
            }
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task ManuallyInsertAsync(User t)
        {
            try
            {
                await _context.Users.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ManuallyInsertAsync for UserId {UserId}", t.Id);
                throw;
            }
        }

        public override async Task UpdateAsync(User t)
        {
            try
            {
                _context.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for UserId {UserId}", t.Id);
                throw;
            }
        }

        public override async Task<List<User>> GetRandomUsers(int number)
        {
            try
            {
                return await _context.Users
                    .OrderBy(u => Guid.NewGuid())
                    .Take(number)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetRandomUsers");
                throw;
            }
        }

        public override async Task<int> GetEntryCountAsync(int id)
        {
            return await _context.Entries.CountAsync(e => e.UserId == id);
        }

        public override async Task<int> GetPostCountAsync(int id)
        {
            return await _context.Posts.CountAsync(p => p.UserId == id);
        }

        public override async Task<User> GetBySpecificPropertySingularAsync(Func<IQueryable<User>, IQueryable<User>> queryModifier)
        {
            IQueryable<User> query = _context.Users;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task<List<User>> GetWithCustomSearchAsync(Func<IQueryable<User>, IQueryable<User>> queryModifier)
        {
            IQueryable<User> query = _context.Users;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }

        public override async Task<User> GetUserModuleAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Users
                .Where(user => user.Id == id)
                .Select(user => new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                    Bots = user.Bots,
                    City = user.City,               
                })
                .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
