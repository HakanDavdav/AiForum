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

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Users.AnyAsync(user => user.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckEntity with UserId {UserId}", id);
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

        public override async Task<User> GetByUsernameAsync(string name)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.UserName == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByUsernameAsync with Username {Username}", name);
                throw;
            }
        }

        public override async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByEmailAsync with Email {Email}", email);
                throw;
            }
        }

        public override async Task InsertAsync(User t)
        {
            try
            {
                await _context.Users.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InsertAsync for UserId {UserId}", t.Id);
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

        public override async Task<User> GetByProfileNameAsync(string profileName)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.ProfileName == profileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByProfileNameAsync with ProfileName {ProfileName}", profileName);
                throw;
            }
        }

        public override async Task<User> GetByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByPhoneNumberAsync with PhoneNumber {PhoneNumber}", phoneNumber);
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

        public override Task<List<User>> GetAllWithCustomSearch(Func<IQueryable<User>, IQueryable<User>> queryModifier)
        {
            throw new NotImplementedException();
        }

        public override async Task<int> GetEntryCountOfUserAsync(int id)
        {
            return await _context.Entries.CountAsync(e => e.UserId == id);
        }

        public override async Task<int> GetPostCountOfUserAsync(int id)
        {
            return await _context.Posts.CountAsync(p => p.UserId == id);
        }
    }
}
