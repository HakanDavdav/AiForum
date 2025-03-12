using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class BotRepository : AbstractBotRepository
    {
        public BotRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(Bot t)
        {
            _context.Bots.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<IQueryable<Bot>> GetAllAsync()
        {
            IQueryable<Bot> bots = _context.Bots;
            return bots;
        }

        public override async Task<IQueryable<Bot>> GetAllWithInfoAsync()
        {
            IQueryable<Bot> bots = _context.Bots.Include(bot => bot.Posts)
                                                .ThenInclude(post => post.Likes)
                                                .Include(bot => bot.Entries)
                                                .ThenInclude(entry => entry.Likes)
                                                .Include(bot => bot.Likes)
                                                .Include(bot => bot.Followers)
                                                .Include(bot => bot.Followings)
                                                .Include(bot => bot.User);
            return bots;
        }

        public override async Task<Bot> GetByIdAsync(int id)
        {
            var bot =  await _context.Bots.FirstOrDefaultAsync(bot => bot.BotId == id);
#pragma warning disable CS8603 // Possible null reference return.
            return bot;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task<Bot> GetByIdWithInfoAsync(int id)
        {
            var bot = await _context.Bots.Include(bot => bot.Posts)
                                          .ThenInclude(post => post.Likes)
                                          .Include(bot => bot.Entries)
                                          .ThenInclude(entry => entry.Likes)
                                          .Include(bot => bot.Likes)
                                          .Include(bot => bot.Followers)
                                          .Include(bot => bot.Followings)
                                          .Include(bot => bot.User)
                                          .FirstOrDefaultAsync(bot => bot.BotId == id);
#pragma warning disable CS8603 // Possible null reference return.
            return bot;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task InsertAsync(Bot t)
        {
            _context.Bots.Add(t);
            await _context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(Bot t)
        {
            _context.Update(t);
            await _context.SaveChangesAsync();
        }
    }
}
