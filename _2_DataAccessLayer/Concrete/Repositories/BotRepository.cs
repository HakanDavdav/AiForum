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
    public class BotRepository : AbstractBotRepository
    {
        public BotRepository(ApplicationDbContext context, ILogger<Bot> logger) : base(context, logger)
        {
        }

        public override async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public override async Task DeleteAsync(Bot t)
        {
            try
            {
                _context.Bots.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for BotId {BotId}", t.BotId);
                throw;
            }
        }


        public override async Task<Bot> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Bots.FirstOrDefaultAsync(bot => bot.BotId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with BotId {BotId}", id);
                throw;
            }
        }


        public override async Task ManuallyInsertAsync(Bot t)
        {
            try
            {
                await _context.Bots.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ManuallyInsertAsync for BotId {BotId}", t.BotId);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for BotId {BotId}", t.BotId);
                throw;
            }
        }

        public override async Task<List<Bot>> GetWithCustomSearchAsync(Func<IQueryable<Bot>, IQueryable<Bot>> queryModifier)
        {
            IQueryable<Bot> query = _context.Bots;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }

        public override async Task<Bot> GetBySpecificPropertySingularAsync(Func<IQueryable<Bot>, IQueryable<Bot>> queryModifier)
        {
            IQueryable<Bot> query = _context.Bots;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task<Bot> GetBotModuleAsync(int id)
        {
            var bot = await _context.Bots.Where(bot => bot.BotId == id).Select(
                bot => new Bot
                {
                    BotId = bot.BotId,
                    BotGrade = bot.BotGrade,
                    BotProfileName = bot.BotProfileName,
                    User = bot.User,
                    UserId = bot.UserId,
                }).FirstOrDefaultAsync();
            return bot;
        }

        public async override Task<int> GetFollowerCountAsync(int id)
        {
            return await _context.Follows.CountAsync(follow => follow.BotFollowedId == id);
        }

        public async override Task<int> GetFollowedCountAsync(int id)
        {
            return await _context.Follows.CountAsync(follow => follow.BotFollowerId == id);
        }
    }
}
