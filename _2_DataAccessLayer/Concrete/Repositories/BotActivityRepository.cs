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
    public class BotActivityRepository : AbstractActivityRepository
    {
        public BotActivityRepository(ApplicationDbContext context, ILogger<BotActivity> logger) : base(context, logger)
        {
        }

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Activities.AnyAsync(activity => activity.ActivityId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckEntity with ActivityId {ActivityId}", id);
                throw;
            }
        }

        public override async Task DeleteAsync(BotActivity t)
        {
            try
            {
                _context.Activities.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for ActivityId {ActivityId}", t.ActivityId);
                throw;
            }
        }

        public override async Task<List<BotActivity>> GetAllByBotIdAsync(int id)
        {
            try
            {
                var query = _context.Activities.Where(activity => activity.BotId == id);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllByBotIdWithIntervalAsync with BotId {BotId}", id);
                throw;
            }
        }
        public override Task<List<BotActivity>> GetAllWithCustomSearch(Func<IQueryable<BotActivity>, IQueryable<BotActivity>> queryModifier)
        {
            throw new NotImplementedException();
        }

        public override async Task<BotActivity> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Activities.FirstOrDefaultAsync(activity => activity.ActivityId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with ActivityId {ActivityId}", id);
                throw;
            }
        }

        public override async Task InsertAsync(BotActivity t)
        {
            try
            {
                await _context.Activities.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InsertAsync for ActivityId {ActivityId}", t.ActivityId);
                throw;
            }
        }

        public override async Task UpdateAsync(BotActivity t)
        {
            try
            {
                _context.Activities.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for ActivityId {ActivityId}", t.ActivityId);
                throw;
            }
        }
    }
}
