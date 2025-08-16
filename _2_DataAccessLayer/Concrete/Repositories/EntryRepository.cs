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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in CheckEntity with EntryId {EntryId}", id);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in DeleteAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
        }

        public override async Task<List<Entry>> GetAllByBotIdWithIntervalAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                return await _context.Entries
                    .Where(entry => entry.BotId == id)
                    .OrderByDescending(entry=> entry.DateTime) 
                    .Skip(startInterval)
                    .Take(endInterval)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in GetAllByBotIdWithIntervalAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetAllByPostIdWithIntervalAsync(int id, int intervalStart, int intervalEnd)
        {
            try
            {
                return await _context.Entries
                    .Where(entry => entry.PostId == id)
                    .OrderByDescending(entry => entry.DateTime)
                    .Skip(intervalStart)
                    .Take(intervalEnd)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in GetAllByPostIdWithIntervalAsync with PostId {PostId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetAllByUserIdWithIntervalsAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                return await _context.Entries
                    .Where(entry => entry.UserId == id)
                    .OrderByDescending(entry => entry.DateTime)
                    .Skip(startInterval)
                    .Take(endInterval)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in GetAllByUserIdWithIntervalsAsync with UserId {UserId}", id);
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
                return await _context.Entries.FirstOrDefaultAsync(entry => entry.EntryId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in GetByIdAsync with EntryId {EntryId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetRandomEntriesByBotId(int id, int number)
        {
            try
            {
                return await _context.Entries
                    .Where(entry => entry.BotId == id)
                    .OrderBy(e => Guid.NewGuid())
                    .Take(number)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in GetRandomEntriesByBotId with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetRandomEntriesByPostId(int id, int number)
        {
            try
            {
                return await _context.Entries
                    .Where(entry => entry.PostId == id)
                    .OrderBy(e => Guid.NewGuid())
                    .Take(number)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in GetRandomEntriesByPostId with PostId {PostId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetRandomEntriesByUserId(int id, int number)
        {
            try
            {
                return await _context.Entries
                    .Where(entry => entry.UserId == id)
                    .OrderBy(e => Guid.NewGuid())
                    .Take(number)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in GetRandomEntriesByUserId with UserId {UserId}", id);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in InsertAsync for EntryId {EntryId}", t.EntryId);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in UpdateAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
        }
    }
}
