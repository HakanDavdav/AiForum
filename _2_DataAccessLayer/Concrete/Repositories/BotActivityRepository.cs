using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public override async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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

        public override async Task ManuallyInsertAsync(BotActivity t)
        {
            try
            {
                await _context.Activities.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ManuallyInsertAsync for ActivityId {ActivityId}", t.ActivityId);
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

        public override async Task<List<BotActivity>> GetWithCustomSearchAsync(Func<IQueryable<BotActivity>, IQueryable<BotActivity>> queryModifier)
        {
            IQueryable<BotActivity> query = _context.Activities;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }


        public override async Task<BotActivity> GetBySpecificPropertySingularAsync(Func<IQueryable<BotActivity>, IQueryable<BotActivity>> queryModifier)
        {
            IQueryable<BotActivity> query = _context.Activities;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override Task<List<BotActivity>> GetBotActivityModulesForBotAsync(int botId, int startInterval, int endInterval)
        {
            var BotActivities = _context.Activities.Where(activity => activity.OwnerBotId == botId).Skip(startInterval).Take(endInterval - startInterval).Select(
                activity => new BotActivity
                {
                    ActivityId = activity.ActivityId,
                    OwnerBotId = activity.OwnerBotId,
                    BotActivityType = activity.BotActivityType,
                    IsRead = activity.IsRead,
                    AdditionalId = activity.AdditionalId,
                    DateTime = activity.DateTime,
                    OwnerBot = activity.OwnerBot,
                    RelatedUser = activity.RelatedUser,
                });
            return BotActivities.ToListAsync();
        }

        public override Task<List<BotActivity>> GetBotActivityModulesForUserAsync(int userId, int startInterval, int endInterval)
        {
            var BotActivities = _context.Activities.Where(activity => activity.RelatedUserId == userId).Skip(startInterval).Take(endInterval - startInterval).Select(
                activity => new BotActivity
                {
                    ActivityId = activity.ActivityId,
                    OwnerBotId = activity.OwnerBotId,
                    BotActivityType = activity.BotActivityType,
                    IsRead = activity.IsRead,
                    AdditionalId = activity.AdditionalId,
                    DateTime = activity.DateTime,
                    OwnerBot = activity.OwnerBot,
                    RelatedUser = activity.RelatedUser,
                });
            return BotActivities.ToListAsync();
        }


        public override async Task ManuallyInsertRangeAsync(List<BotActivity> activities)
        {
            _context.Activities.AddRange(activities);
            await _context.SaveChangesAsync();
        }


    }
}
