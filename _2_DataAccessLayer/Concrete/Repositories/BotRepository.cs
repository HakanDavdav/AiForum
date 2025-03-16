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
            try
            {
                _context.Bots.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL kaynaklı hatalar (Bağlantı hatası, timeout, syntax hatası vb.)
                Console.WriteLine($"SQL Hatası: {sqlEx.Message}");
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Geçersiz işlem hatası (Context kapalı, nesne takibi sorunu vb.)
                Console.WriteLine($"Geçersiz İşlem Hatası: {invalidOpEx.Message}");
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Veritabanı güncelleme hatası (FK, Unique Key ihlali vb.)
                Console.WriteLine($"Veritabanı Güncelleme Hatası: {dbUpdateEx.Message}");
            }
        }


        public override async Task<List<Bot>> GetAllByUserIdAsync(int id)
        {
            try
            {
                IQueryable<Bot> bots = _context.Bots.Where(bot => bot.UserId == id);
                return await bots.ToListAsync();
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

      

        public override async Task<Bot> GetByIdAsync(int id)
        {
            try
            {
                var bot = await _context.Bots.FirstOrDefaultAsync(bot => bot.BotId == id);
#pragma warning disable CS8603 // Possible null reference return.
                return bot;
#pragma warning restore CS8603 // Possible null reference return.
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

        public override async Task<List<Bot>> GetRandomBots(int number)
        {
            try
            {
                IQueryable<Bot> bot = _context.Bots.OrderBy(bot => Guid.NewGuid()).Take(number);
                return await bot.ToListAsync();
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

        public override async Task InsertAsync(Bot t)
        {
            try
            {
                _context.Bots.Add(t);
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

        public override async Task UpdateAsync(Bot t)
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
