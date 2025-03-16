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
    public class LikeRepository : AbstractLikeRepository
    {
        public LikeRepository(ApplicationDbContext context) : base(context)
        {
        }



        public override async Task DeleteAsync(Like t)
        {
            try
            {
                _context.Likes.Remove(t);
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

        public override async Task<List<Like>> GetAllByBotIdAsync(int id)
        {
            try
            {
                IQueryable<Like> likes = _context.Likes.Where(like => like.BotId == id);
                return await likes.ToListAsync();
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

        public override async Task<List<Like>> GetAllByUserIdAsync(int id)
        {
            try
            {
                IQueryable<Like> likes = _context.Likes.Where(like => like.UserId == id);
                return await likes.ToListAsync();
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


        public override async Task<Like> GetByIdAsync(int id)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Like like = await _context.Likes.FirstOrDefaultAsync(like => like.LikeId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                return like;
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


        public override async Task InsertAsync(Like t)
        {
            try
            {
                await _context.Likes.AddAsync(t);
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


        public override async Task UpdateAsync(Like t)
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
