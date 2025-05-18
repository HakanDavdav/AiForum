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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in CheckEntity with UserId {UserId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in CheckEntity with UserId {UserId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in CheckEntity with UserId {UserId}", id);
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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in DeleteAsync for UserId {UserId}", t.Id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in DeleteAsync for UserId {UserId}", t.Id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in DeleteAsync for UserId {UserId}", t.Id);
                throw;
            }
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByIdAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task<User> GetByUsernameAsync(string name)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.UserName == name);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByUsernameAsync with Username {Username}", name);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByUsernameAsync with Username {Username}", name);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByUsernameAsync with Username {Username}", name);
                throw;
            }
        }

        public override async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByEmailAsync with Email {Email}", email);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByEmailAsync with Email {Email}", email);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByEmailAsync with Email {Email}", email);
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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in InsertAsync for UserId {UserId}", t.Id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in InsertAsync for UserId {UserId}", t.Id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in InsertAsync for UserId {UserId}", t.Id);
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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in UpdateAsync for UserId {UserId}", t.Id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in UpdateAsync for UserId {UserId}", t.Id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in UpdateAsync for UserId {UserId}", t.Id);
                throw;
            }
        }

        public override async Task<User> GetByProfileNameAsync(string profileName)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.ProfileName == profileName);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByProfileNameAsync with ProfileName {ProfileName}", profileName);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByProfileNameAsync with ProfileName {ProfileName}", profileName);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByProfileNameAsync with ProfileName {ProfileName}", profileName);
                throw;
            }
        }

        public override async Task<User> GetByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByPhoneNumberAsync with PhoneNumber {PhoneNumber}", phoneNumber);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByPhoneNumberAsync with PhoneNumber {PhoneNumber}", phoneNumber);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByPhoneNumberAsync with PhoneNumber {PhoneNumber}", phoneNumber);
                throw;
            }
        }

        public override async Task<List<User>> GetRandomUsers(int number)
        {
            try
            {
                IQueryable<User> users = _context.Users.OrderBy(user => Guid.NewGuid()).Take(number);
                return await users.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetRandomUsers");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetRandomUsers");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetRandomUsers");
                throw;
            }
        }

        public override Task<List<User>> GetAllWithCustomSearch(Func<IQueryable<User>, IQueryable<User>> queryModifier)
        {
            throw new NotImplementedException();
        }

        public override async Task<int> GetEntryCount(int id)
        {
            var entryCount = await _context.Entries.CountAsync(e => e.UserId == id);
            return entryCount;
        }

        public override async Task<int> GetPostCount(int id)
        {
            var postCount = await _context.Posts.CountAsync(p => p.UserId == id);
            return postCount;
        }
    }
}
