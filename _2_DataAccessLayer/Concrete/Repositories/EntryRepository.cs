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

            try
            {
                _context.Entries.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL kaynaklı hatalar (Bağlantı hatası, timeout, syntax hatası vb.)
                Console.WriteLine($"SQL Hatası: {sqlEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Geçersiz işlem hatası (Context kapalı, nesne takibi sorunu vb.)
                Console.WriteLine($"Geçersiz İşlem Hatası: {invalidOpEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Veritabanı güncelleme hatası (FK, Unique Key ihlali vb.)
                Console.WriteLine($"Veritabanı Güncelleme Hatası: {dbUpdateEx.Message}");
                throw; // Hata yeniden fırlatılır
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
                // SQL kaynaklı hatalar (Bağlantı hatası, timeout, syntax hatası vb.)
                Console.WriteLine($"SQL Hatası: {sqlEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Geçersiz işlem hatası (Context kapalı, nesne takibi sorunu vb.)
                Console.WriteLine($"Geçersiz İşlem Hatası: {invalidOpEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Veritabanı güncelleme hatası (FK, Unique Key ihlali vb.)
                Console.WriteLine($"Veritabanı Güncelleme Hatası: {dbUpdateEx.Message}");
                throw; // Hata yeniden fırlatılır
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
                // SQL kaynaklı hatalar (Bağlantı hatası, timeout, syntax hatası vb.)
                Console.WriteLine($"SQL Hatası: {sqlEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Geçersiz işlem hatası (Context kapalı, nesne takibi sorunu vb.)
                Console.WriteLine($"Geçersiz İşlem Hatası: {invalidOpEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Veritabanı güncelleme hatası (FK, Unique Key ihlali vb.)
                Console.WriteLine($"Veritabanı Güncelleme Hatası: {dbUpdateEx.Message}");
                throw; // Hata yeniden fırlatılır
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
                // SQL kaynaklı hatalar (Bağlantı hatası, timeout, syntax hatası vb.)
                Console.WriteLine($"SQL Hatası: {sqlEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Geçersiz işlem hatası (Context kapalı, nesne takibi sorunu vb.)
                Console.WriteLine($"Geçersiz İşlem Hatası: {invalidOpEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Veritabanı güncelleme hatası (FK, Unique Key ihlali vb.)
                Console.WriteLine($"Veritabanı Güncelleme Hatası: {dbUpdateEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
        }

     

        public override async Task<Entry> GetByIdAsync(int id)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Entry entry = await _context.Entries.FirstOrDefaultAsync(entry => entry.EntryId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                return entry;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL kaynaklı hatalar (Bağlantı hatası, timeout, syntax hatası vb.)
                Console.WriteLine($"SQL Hatası: {sqlEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Geçersiz işlem hatası (Context kapalı, nesne takibi sorunu vb.)
                Console.WriteLine($"Geçersiz İşlem Hatası: {invalidOpEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Veritabanı güncelleme hatası (FK, Unique Key ihlali vb.)
                Console.WriteLine($"Veritabanı Güncelleme Hatası: {dbUpdateEx.Message}");
                throw; // Hata yeniden fırlatılır
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
                // SQL kaynaklı hatalar (Bağlantı hatası, timeout, syntax hatası vb.)
                Console.WriteLine($"SQL Hatası: {sqlEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Geçersiz işlem hatası (Context kapalı, nesne takibi sorunu vb.)
                Console.WriteLine($"Geçersiz İşlem Hatası: {invalidOpEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Veritabanı güncelleme hatası (FK, Unique Key ihlali vb.)
                Console.WriteLine($"Veritabanı Güncelleme Hatası: {dbUpdateEx.Message}");
                throw; // Hata yeniden fırlatılır
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
                // SQL kaynaklı hatalar (Bağlantı hatası, timeout, syntax hatası vb.)
                Console.WriteLine($"SQL Hatası: {sqlEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Geçersiz işlem hatası (Context kapalı, nesne takibi sorunu vb.)
                Console.WriteLine($"Geçersiz İşlem Hatası: {invalidOpEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Veritabanı güncelleme hatası (FK, Unique Key ihlali vb.)
                Console.WriteLine($"Veritabanı Güncelleme Hatası: {dbUpdateEx.Message}");
                throw; // Hata yeniden fırlatılır
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
                // SQL kaynaklı hatalar (Bağlantı hatası, timeout, syntax hatası vb.)
                Console.WriteLine($"SQL Hatası: {sqlEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Geçersiz işlem hatası (Context kapalı, nesne takibi sorunu vb.)
                Console.WriteLine($"Geçersiz İşlem Hatası: {invalidOpEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Veritabanı güncelleme hatası (FK, Unique Key ihlali vb.)
                Console.WriteLine($"Veritabanı Güncelleme Hatası: {dbUpdateEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
        }



        //would prefer _userManager
        public override async Task InsertAsync(Entry t)
        {

            try
            {
                await _context.Entries.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL kaynaklı hatalar (Bağlantı hatası, timeout, syntax hatası vb.)
                Console.WriteLine($"SQL Hatası: {sqlEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Geçersiz işlem hatası (Context kapalı, nesne takibi sorunu vb.)
                Console.WriteLine($"Geçersiz İşlem Hatası: {invalidOpEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Veritabanı güncelleme hatası (FK, Unique Key ihlali vb.)
                Console.WriteLine($"Veritabanı Güncelleme Hatası: {dbUpdateEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
        }

        public override async Task UpdateAsync(Entry t)
        {
            try
            {
                _context.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL kaynaklı hatalar (Bağlantı hatası, timeout, syntax hatası vb.)
                Console.WriteLine($"SQL Hatası: {sqlEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Geçersiz işlem hatası (Context kapalı, nesne takibi sorunu vb.)
                Console.WriteLine($"Geçersiz İşlem Hatası: {invalidOpEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Veritabanı güncelleme hatası (FK, Unique Key ihlali vb.)
                Console.WriteLine($"Veritabanı Güncelleme Hatası: {dbUpdateEx.Message}");
                throw; // Hata yeniden fırlatılır
            }
        }


    }
}
