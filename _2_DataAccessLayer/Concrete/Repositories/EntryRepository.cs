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

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Entries.AnyAsync(entry => entry.EntryId == id);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in CheckEntity: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in CheckEntity: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in CheckEntity: {dbUpdateEx.Message}");
                throw;
            }

        }
        public override async Task DeleteAsync(Entry t)
        {
            try
            {
                _context.Entries.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in DeleteAsync: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in DeleteAsync: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in DeleteAsync: {dbUpdateEx.Message}");
                throw;
            }
        }

        public override async Task<List<Entry>> GetAllByBotIdAsync(int id)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.BotId == id);
                return await entries.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetAllByBotIdAsync: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetAllByBotIdAsync: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetAllByBotIdAsync: {dbUpdateEx.Message}");
                throw;
            }
        }

        public override async Task<List<Entry>> GetAllByPostId(int id)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.PostId == id);
                return await entries.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetAllByPostId: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetAllByPostId: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetAllByPostId: {dbUpdateEx.Message}");
                throw;
            }
        }

        public override async Task<List<Entry>> GetAllByUserIdAsync(int id)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.UserId == id);
                return await entries.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetAllByUserOrBotIdAsFollowerAsync: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetAllByUserOrBotIdAsFollowerAsync: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetAllByUserOrBotIdAsFollowerAsync: {dbUpdateEx.Message}");
                throw;
            }
        }

        public override Task<List<Entry>> GetAllWithCustomSearch(Func<IQueryable<Entry>, IQueryable<Entry>> queryModifier)
        {
            throw new NotImplementedException();
        }

        public override async Task<Entry> GetByIdAsync(int id)
        {
            try
            {
                Entry entry = await _context.Entries.FirstOrDefaultAsync(entry => entry.EntryId == id);
                return entry;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetByIdAsync: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetByIdAsync: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetByIdAsync: {dbUpdateEx.Message}");
                throw;
            }
        }

        public override async Task<List<Entry>> GetRandomEntriesByBotId(int id, int number)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.BotId == id).OrderBy(entry => Guid.NewGuid()).Take(number);
                return await entries.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetRandomEntriesByBotId: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetRandomEntriesByBotId: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetRandomEntriesByBotId: {dbUpdateEx.Message}");
                throw;
            }
        }

        public override async Task<List<Entry>> GetRandomEntriesByPostId(int id, int number)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.PostId == id).OrderBy(entry => Guid.NewGuid()).Take(number);
                return await entries.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetRandomEntriesByPostId: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetRandomEntriesByPostId: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetRandomEntriesByPostId: {dbUpdateEx.Message}");
                throw;
            }
        }

        public override async Task<List<Entry>> GetRandomEntriesByUserId(int id, int number)
        {
            try
            {
                IQueryable<Entry> entries = _context.Entries.Where(entry => entry.UserId == id).OrderBy(entry => Guid.NewGuid()).Take(number);
                return await entries.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetRandomEntriesByUserId: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetRandomEntriesByUserId: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetRandomEntriesByUserId: {dbUpdateEx.Message}");
                throw;
            }
        }

        public override async Task InsertAsync(Entry t)
        {
            try
            {
                await _context.Entries.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in InsertAsync: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in InsertAsync: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in InsertAsync: {dbUpdateEx.Message}");
                throw;
            }
        }

        public override async Task UpdateAsync(Entry t)
        {
            try
            {
                _context.Entries.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in UpdateAsync: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in UpdateAsync: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in UpdateAsync: {dbUpdateEx.Message}");
                throw;
            }
        }
    }
}
