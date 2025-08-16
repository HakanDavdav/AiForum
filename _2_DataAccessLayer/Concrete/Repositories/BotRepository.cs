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

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Bots.AnyAsync(bot => bot.BotId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckEntity with BotId {BotId}", id);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for BotId {BotId}", t.BotId);
                throw;
            }
        }

        public override async Task<List<Bot>> GetAllByUserIdAsync(int id)
        {
            try
            {
                var query = _context.Bots.Where(bot => bot.UserId == id);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllByUserIdWithIntervalsAsync with UserId {UserId}", id);
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
                return await _context.Bots.FirstOrDefaultAsync(bot => bot.BotId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<int> GetEntryCountOfBotAsync(int id)
        {
            try
            {
                return await _context.Entries.CountAsync(entry => entry.BotId == id);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error in  GetEntryCountOfBotAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<int> GetPostCountOfBotAsync(int id)
        {
            try
            {
                return await _context.Posts.CountAsync(post => post.BotId == id);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error in  GetPostCountOfBotAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Bot>> GetRandomBots(int number)
        {
            try
            {
                var query = _context.Bots.OrderBy(bot => Guid.NewGuid()).Take(number);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetRandomBots");
                throw;
            }
        }

        public override async Task InsertAsync(Bot t)
        {
            try
            {
                await _context.Bots.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InsertAsync for BotId {BotId}", t.BotId);
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
    }
}
