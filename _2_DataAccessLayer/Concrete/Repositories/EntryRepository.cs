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


        public override async Task<List<Entry>> GetAllAsync()
        {
            IQueryable<Entry> allEntries = _context.Entries;                                                
            return await allEntries.ToListAsync();
        }

        public override async Task<List<Entry>> GetAllWithInfoAsync()
        {
            IQueryable<Entry> allEntries = _context.Entries
                                                   .Include(entry => entry.Likes)
                                                   .ThenInclude(like => like.User)
                                                   .Include(entry => entry.Likes)
                                                   .ThenInclude(like => like.Bot)
                                                   .Include(entry => entry.User)
                                                   .Include(entry => entry.Bot);
            return await allEntries.ToListAsync();
        }

        public override async Task<Entry> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Entry entry = await _context.Entries.FirstOrDefaultAsync(entry => entry.EntryId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return entry;
        }

        public override async Task<Entry> GetByIdWithInfoAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Entry entry = await _context.Entries.Include(entry => entry.Likes)
                                                .ThenInclude(like => like.User)
                                                .Include(entry => entry.Likes)
                                                .ThenInclude(like => like.Bot)
                                                .Include(entry => entry.User)
                                                .Include(entry => entry.Bot)
                                                .FirstOrDefaultAsync(entry => entry.EntryId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return entry;

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
