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
    public class NewsRepository : AbstractNewsRepository
    {
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.News.AnyAsync(news => news.NewsId == id);
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
        public override async Task DeleteAsync(News t)
        {
            try
            {
                _context.Remove(t);
                await _context.SaveChangesAsync(); 
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in DeleteAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in DeleteAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in DeleteAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override Task<List<News>> GetAllWithCustomSearch(Func<IQueryable<News>, IQueryable<News>> queryModifier)
        {
            throw new NotImplementedException();
        }

        public override async Task<News> GetByIdAsync(int id)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                News news = await _context.News.FirstOrDefaultAsync(news => news.NewsId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                return news; 

            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetByIdAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetByIdAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetByIdAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task<List<News>> GetRandomNews(int number)
        {
            try
            {
                IQueryable<News> news = _context.News.OrderBy(news => Guid.NewGuid()).Take(number); 
                return await news.ToListAsync(); 
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetRandomNews: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetRandomNews: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetRandomNews: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task InsertAsync(News t)
        {
            try
            {
                await _context.News.AddAsync(t); 
                await _context.SaveChangesAsync(); 
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in InsertAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in InsertAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in InsertAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override Task UpdateAsync(News t)
        {
            throw new NotImplementedException(); // Not implemented exception
        }
    }
}
