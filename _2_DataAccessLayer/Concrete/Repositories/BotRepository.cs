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
                _logger.LogError(ex, "Error in DeleteAsync for ParentBotId {ParentBotId}", t.Id);
                throw;
            }
        }


        public override async Task<Bot> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null) return null;
                return await _context.Bots.FirstOrDefaultAsync(bot => bot.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with ParentBotId {ParentBotId}", id);
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
                _logger.LogError(ex, "Error in ManuallyInsertAsync for ParentBotId {ParentBotId}", t.Id);
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
                _logger.LogError(ex, "Error in UpdateAsync for ParentBotId {ParentBotId}", t.Id);
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
            var bot = await _context.Bots.Where(bot => bot.Id == id).Select(
                bot => new Bot
                {
                    Id = bot.Id,
                    BotGrade = bot.BotGrade,
                    BotProfileName = bot.BotProfileName,
                    ImageUrl = bot.ImageUrl,
                    DateTime = bot.DateTime,
                    FollowerCount = bot.FollowerCount,
                    FollowedCount = bot.FollowedCount,
                    EntryCount = bot.EntryCount,
                    PostCount = bot.PostCount,
                    LikeCount = bot.LikeCount,
                    Mode = bot.Mode,
                    ParentUser = bot.ParentUser,
                    ParentUserId = bot.ParentUserId,
                    ParentBot = bot.ParentBot,
                    ParentBotId = bot.ParentBotId,
                }).FirstOrDefaultAsync();
            return bot;
        }

        public override async Task ManuallyInsertRangeAsync(List<Bot> bots)
        {
            _context.Bots.AddRange(bots);
            await _context.SaveChangesAsync();
        }

        public override async Task<Bot> GetBotWithChildBotTree(int id)
        {
            var parentBot = await _context.Bots
                             .Where(u => u.Id == id)
                             .Include(u => u.ChildBots)
                             .FirstOrDefaultAsync();

            if (parentBot == null)
                throw new InvalidOperationException($"Bot with id {id} not found.");


            foreach (var bot in parentBot.ChildBots)
            {
                await CollectBotsTreeAsync(bot);
            }

            return parentBot;

            async Task CollectBotsTreeAsync(Bot bot)
            {

                // Properly await loading child bots
                await _context.Entry(bot)
                              .Collection(b => b.ChildBots)
                              .LoadAsync();

                foreach (var childBot in bot.ChildBots)
                {
                    await CollectBotsTreeAsync(childBot);
                }
            }
        }
    }
}
