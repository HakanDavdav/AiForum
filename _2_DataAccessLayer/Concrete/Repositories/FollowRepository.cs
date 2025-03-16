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
    public class FollowRepository : AbstractFollowRepository
    {
        public FollowRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(Follow t)
        {
            try
            {
                _context.Follows.Remove(t);
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

        public override async Task<List<Follow>> GetAllByBotIdAsync(int id)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.BotFolloweeId == id);
                return await follows.ToListAsync();
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

        public override async Task<List<Follow>> GetAllByUserIdAsync(int id)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.FolloweeId == id);
                return await follows.ToListAsync();
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


        public override async Task<Follow> GetByIdAsync(int id)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Follow follow = await _context.Follows.FirstOrDefaultAsync(follow => follow.FollowId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                return follow;
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


        public override async Task InsertAsync(Follow t)
        {
            try
            {
                await _context.Follows.AddAsync(t);
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

        public override async Task UpdateAsync(Follow t)
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
