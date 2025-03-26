using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.Error(sqlEx, "SQL Error in CheckEntity with ActivityId {ActivityId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.Error(invalidOpEx, "Invalid Operation Error in CheckEntity with ActivityId {ActivityId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.Error(dbUpdateEx, "Database Update Error in CheckEntity with ActivityId {ActivityId}", id);
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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.Error(sqlEx, "SQL Error in DeleteAsync for ActivityId {ActivityId}", t.ActivityId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.Error(invalidOpEx, "Invalid Operation Error in DeleteAsync for ActivityId {ActivityId}", t.ActivityId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.Error(dbUpdateEx, "Database Update Error in DeleteAsync for ActivityId {ActivityId}", t.ActivityId);
                throw;
            }
        }

        public override async Task<List<BotActivity>> GetAllByBotIdAsync(int id)
        {
            try
            {
                IQueryable<BotActivity> botActivities = _context.Activities.Where(activity => activity.BotId == id);
                return await botActivities.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.Error(sqlEx, "SQL Error in GetAllByBotIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.Error(invalidOpEx, "Invalid Operation Error in GetAllByBotIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.Error(dbUpdateEx, "Database Update Error in GetAllByBotIdAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<BotActivity>> GetAllByUserIdAsync(int id)
        {
            try
            {
                IQueryable<BotActivity> botActivities = _context.Activities.Where(activity => activity.ActivityId == id);
                return await botActivities.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.Error(sqlEx, "SQL Error in GetAllByUserIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.Error(invalidOpEx, "Invalid Operation Error in GetAllByUserIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.Error(dbUpdateEx, "Database Update Error in GetAllByUserIdAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task<BotActivity> GetByIdAsync(int id)
        {
            try
            {
                var activity = await _context.Activities.FirstOrDefaultAsync(activity => activity.ActivityId == id);
                return activity;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.Error(sqlEx, "SQL Error in GetByIdAsync with ActivityId {ActivityId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.Error(invalidOpEx, "Invalid Operation Error in GetByIdAsync with ActivityId {ActivityId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.Error(dbUpdateEx, "Database Update Error in GetByIdAsync with ActivityId {ActivityId}", id);
                throw;
            }
        }

        public override async Task InsertAsync(BotActivity t)
        {
            try
            {
                _context.Activities.Add(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.Error(sqlEx, "SQL Error in InsertAsync for ActivityId {ActivityId}", t.ActivityId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.Error(invalidOpEx, "Invalid Operation Error in InsertAsync for ActivityId {ActivityId}", t.ActivityId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.Error(dbUpdateEx, "Database Update Error in InsertAsync for ActivityId {ActivityId}", t.ActivityId);
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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.Error(sqlEx, "SQL Error in UpdateAsync for ActivityId {ActivityId}", t.ActivityId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.Error(invalidOpEx, "Invalid Operation Error in UpdateAsync for ActivityId {ActivityId}", t.ActivityId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.Error(dbUpdateEx, "Database Update Error in UpdateAsync for ActivityId {ActivityId}", t.ActivityId);
                throw;
            }
        }
    }
}
