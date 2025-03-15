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


        public override async Task<List<Bot>> GetAllByUserIdAsync(int id)
        {
            IQueryable<Bot> bots = _context.Bots.Where(bot => bot.UserId == id);
            return await bots.ToListAsync();
        }

      

        public override async Task<Bot> GetByIdAsync(int id)
        {
            var bot =  await _context.Bots.FirstOrDefaultAsync(bot => bot.BotId == id);
#pragma warning disable CS8603 // Possible null reference return.
            return bot;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task<List<Bot>> GetRandomBots(int number)
        {
            IQueryable<Bot> bot = _context.Bots.OrderBy(bot => Guid.NewGuid()).Take(number);
            return await bot.ToListAsync();
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
