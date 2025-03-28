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
    public class EntryRepository : AbstractEntryRepository
    {
        public EntryRepository(ApplicationDbContext context, ILogger<Entry> logger) : base(context, logger)
        {
        }

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Entries.AnyAsync(entry => entry.EntryId == id);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in CheckEntity with EntryId {EntryId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in CheckEntity with EntryId {EntryId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in CheckEntity with EntryId {EntryId}", id);
                throw;
            }
        }

        public override async Task DeleteAsync(Entry t)
        {
            try
            {
                _context.Entries.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in DeleteAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in DeleteAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in DeleteAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
        }

        public override async Task<List<Entry>> GetAllByBotIdAsync(int id)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.BotId == id);
                return await entries.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetAllByBotIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetAllByBotIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetAllByBotIdAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetAllByPostId(int id)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.PostId == id);
                return await entries.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetAllByPostIdAsync with PostId {PostId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetAllByPostIdAsync with PostId {PostId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetAllByPostIdAsync with PostId {PostId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetAllByUserIdAsync(int id)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.UserId == id);
                return await entries.ToListAsync();
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

        public override Task<List<Entry>> GetAllWithCustomSearch(Func<IQueryable<Entry>, IQueryable<Entry>> queryModifier)
        {
            throw new NotImplementedException();
        }

        public override async Task<Entry> GetByIdAsync(int id)
        {
            try
            {
                Entry entry = await _context.Entries.FirstOrDefaultAsync(entry => entry.EntryId == id);
                return entry;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByIdAsync with EntryId {EntryId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByIdAsync with EntryId {EntryId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByIdAsync with EntryId {EntryId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetRandomEntriesByBotId(int id, int number)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.BotId == id).OrderBy(entry => Guid.NewGuid()).Take(number);
                return await entries.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetRandomEntriesByBotId with BotId {BotId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetRandomEntriesByBotId with BotId {BotId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetRandomEntriesByBotId with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetRandomEntriesByPostId(int id, int number)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.PostId == id).OrderBy(entry => Guid.NewGuid()).Take(number);
                return await entries.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetRandomEntriesByPostId with PostId {PostId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetRandomEntriesByPostId with PostId {PostId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetRandomEntriesByPostId with PostId {PostId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetRandomEntriesByUserId(int id, int number)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.UserId == id).OrderBy(entry => Guid.NewGuid()).Take(number);
                return await entries.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetRandomEntriesByUserId with UserId {UserId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetRandomEntriesByUserId with UserId {UserId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetRandomEntriesByUserId with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task InsertAsync(Entry t)
        {
            try
            {
                await _context.Entries.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in InsertAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in InsertAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in InsertAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
        }

        public override async Task UpdateAsync(Entry t)
        {
            try
            {
                _context.Entries.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in UpdateAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in UpdateAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in UpdateAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
        }
    }
}
