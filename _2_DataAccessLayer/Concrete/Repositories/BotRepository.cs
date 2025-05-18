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
    public class BotRepository : AbstractBotRepository
    {
        public BotRepository(ApplicationDbContext context, ILogger<Bot> logger) : base(context, logger)
        {
        }

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Bots.AnyAsync(bot => bot.BotId == id);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in CheckEntity with BotId {BotId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in CheckEntity with BotId {BotId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in CheckEntity with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task DeleteAsync(Bot t)
        {
            try
            {
                _context.Bots.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in DeleteAsync for BotId {BotId}", t.BotId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in DeleteAsync for BotId {BotId}", t.BotId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in DeleteAsync for BotId {BotId}", t.BotId);
                throw;
            }
        }

        public override async Task<List<Bot>> GetAllByUserIdAsync(int id)
        {
            try
            {
                IQueryable<Bot> bots = _context.Bots.Where(bot => bot.UserId == id);
                return await bots.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetAllByUserIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetAllByUserIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetAllByUserIdAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override Task<List<Bot>> GetAllWithCustomSearch(Func<IQueryable<Bot>, IQueryable<Bot>> queryModifier)
        {
            throw new NotImplementedException();
        }

        public override async Task<Bot> GetByIdAsync(int id)
        {
            try
            {
                var bot = await _context.Bots.FirstOrDefaultAsync(bot => bot.BotId == id);
#pragma warning disable CS8603 // Possible null reference return.
                return bot;
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByIdAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<int> GetEntryCount(int id)
        {
            try
            {
                var entryCount = await _context.Entries.CountAsync(entry => entry.BotId == id);
                return entryCount;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByIdAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<int> GetPostCount(int id)
        {
            try
            {
                var postCount = await _context.Posts.CountAsync(post => post.BotId == id);
                return postCount;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByIdAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Bot>> GetRandomBots(int number)
        {
            try
            {
                IQueryable<Bot> bot = _context.Bots.OrderBy(bot => Guid.NewGuid()).Take(number);
                return await bot.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetRandomBots");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetRandomBots");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetRandomBots");
                throw;
            }
        }

        public override async Task InsertAsync(Bot t)
        {
            try
            {
                _context.Bots.Add(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in InsertAsync for BotId {BotId}", t.BotId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in InsertAsync for BotId {BotId}", t.BotId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in InsertAsync for BotId {BotId}", t.BotId);
                throw;
            }
        }

        public override async Task UpdateAsync(Bot t)
        {
            try
            {
                _context.Bots.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in UpdateAsync for BotId {BotId}", t.BotId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in UpdateAsync for BotId {BotId}", t.BotId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in UpdateAsync for BotId {BotId}", t.BotId);
                throw;
            }
        }
    }
}
