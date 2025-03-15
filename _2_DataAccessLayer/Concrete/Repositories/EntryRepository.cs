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
    public class EntryRepository : AbstractEntryRepository
    {
        public EntryRepository(ApplicationDbContext context) : base(context)
        {
        }


        public override async Task DeleteAsync(Entry t)
        {
            _context.Entries.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<Entry>> GetAllByBotIdAsync(int id)
        {
            IQueryable<Entry> entries = _context.Entries.Where(entry => entry.BotId == id);
            return await entries.ToListAsync();
        }

        public override async Task<List<Entry>> GetAllByPostId(int id)
        {
            IQueryable<Entry> entries = _context.Entries.Where(entry => entry.PostId == id);
            return await entries.ToListAsync();
        }

        public override async Task<List<Entry>> GetAllByUserIdAsync(int id)
        {
            IQueryable<Entry> entries = _context.Entries.Where(entry => entry.UserId == id);
            return await entries.ToListAsync();
        }

     

        public override async Task<Entry> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Entry entry = await _context.Entries.FirstOrDefaultAsync(entry => entry.EntryId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return entry;
        }

        public override async Task<List<Entry>> GetRandomEntriesByBotId(int id, int number)
        {
            IQueryable<Entry> entries = _context.Entries.Where(entry => entry.BotId == id).OrderBy(entry => Guid.NewGuid()).Take(number);
            return await entries.ToListAsync();
        }

        public override async Task<List<Entry>> GetRandomEntriesByPostId(int id, int number)
        {
            IQueryable<Entry> entries = _context.Entries.Where(entry => entry.PostId == id).OrderBy(entry => Guid.NewGuid()).Take(number);
            return await entries.ToListAsync();
        }

        public override async Task<List<Entry>> GetRandomEntriesByUserId(int id, int number)
        {
            IQueryable<Entry> entries = _context.Entries.Where(entry => entry.UserId == id).OrderBy(entry => Guid.NewGuid()).Take(number);
            return await entries.ToListAsync();
        }



        //would prefer _userManager
        public override async Task InsertAsync(Entry t)
        {

                await _context.Entries.AddAsync(t);
                await _context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(Entry t)
        {
            _context.Update(t);
            await _context.SaveChangesAsync();
        }


    }
}
