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

        public override async Task<BotActivity> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null) return null;
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
                });
            return BotActivities.ToListAsync();
        }

        public override async Task<List<BotActivity>> GetBotActivityModulesForUserAsync(int id, int startInterval, int endInterval)
        {
            var user = await _context.Users
                             .Where(u => u.Id == id)
                             .Include(u => u.Bots)
                             .FirstOrDefaultAsync();

            if (user == null)
                return new List<BotActivity>();

            var botList = new List<Bot>();

            foreach (var bot in user.Bots)
            {
                await CollectBotsTreeAsync(bot, botList);
            }

            async Task CollectBotsTreeAsync(Bot bot, List<Bot> collectedBots)
            {
                collectedBots.Add(bot);

                // Properly await loading child bots
                await _context.Entry(bot)
                              .Collection(b => b.ChildBots)
                              .LoadAsync();

                foreach (var childBot in bot.ChildBots)
                {
                    await CollectBotsTreeAsync(childBot, collectedBots);
                }
            }

            var botIds = botList.Select(bot =>bot.Id).ToList();
            var BotActivities = _context.Activities.
                Where(activity => activity.OwnerBotId != null && botIds.Contains(activity.OwnerBotId.Value)).Skip(startInterval).Take(endInterval - startInterval).Select(
                activity => new BotActivity
                {
                    ActivityId = activity.ActivityId,
                    OwnerBotId = activity.OwnerBotId,
                    BotActivityType = activity.BotActivityType,
                    IsRead = activity.IsRead,
                    AdditionalId = activity.AdditionalId,
                    DateTime = activity.DateTime,
                    OwnerBot = activity.OwnerBot,
                });
            return await BotActivities.ToListAsync();
        }


        public override async Task ManuallyInsertRangeAsync(List<BotActivity> activities)
        {
            _context.Activities.AddRange(activities);
            await _context.SaveChangesAsync();
        }


    }
}
