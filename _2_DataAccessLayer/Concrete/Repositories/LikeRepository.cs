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
    public class LikeRepository : AbstractLikeRepository
    {
        public LikeRepository(ApplicationDbContext context, ILogger<Like> logger) : base(context, logger)
        {
        }

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Likes.AnyAsync(like =>like.LikeId == id);
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
        public override async Task DeleteAsync(Like t)
        {
            try
            {
                _context.Likes.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in DeleteAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in DeleteAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in DeleteAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task<List<Like>> GetAllByBotIdAsync(int id)
        {
            try
            {
                IQueryable<Like> likes = _context.Likes.Where(like => like.BotId == id);
                return await likes.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetAllByBotIdAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetAllByBotIdAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetAllByBotIdAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task<List<Like>> GetAllByEntryIdAsync(int id)
        {
            try
            {
                IQueryable<Like> likes = _context.Likes.Where(like => like.EntryId == id);
                return await likes.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetAllByBotIdAsFollowerWithInfoAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetAllByBotIdAsFollowerWithInfoAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetAllByBotIdAsFollowerWithInfoAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task<List<Like>> GetAllByPostIdAsync(int id)
        {
            try
            {
                IQueryable<Like> likes = _context.Likes.Where(like => like.PostId == id);
                return await likes.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetAllByBotIdAsFollowerWithInfoAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetAllByBotIdAsFollowerWithInfoAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetAllByBotIdAsFollowerWithInfoAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task<List<Like>> GetAllByUserIdAsync(int id)
        {
            try
            {
                IQueryable<Like> likes = _context.Likes.Where(like => like.UserId == id);
                return await likes.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetAllByBotIdAsFollowerWithInfoAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetAllByBotIdAsFollowerWithInfoAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetAllByBotIdAsFollowerWithInfoAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override Task<List<Like>> GetAllWithCustomSearch(Func<IQueryable<Like>, IQueryable<Like>> queryModifier)
        {
            throw new NotImplementedException();
        }

        public override async Task<Like> GetByIdAsync(int id)
        {
            try
            {
                Like like = await _context.Likes.FirstOrDefaultAsync(like => like.LikeId == id);
                return like;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetByIdAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetByIdAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetByIdAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task InsertAsync(Like t)
        {
            try
            {
                await _context.Likes.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in InsertAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in InsertAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in InsertAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task UpdateAsync(Like t)
        {
            try
            {
                _context.Likes.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in UpdateAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in UpdateAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in UpdateAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }
    }
}
