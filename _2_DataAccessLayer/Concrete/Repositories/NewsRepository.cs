using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class NewsRepository : AbstractNewsRepository
    {
        public NewsRepository(ApplicationDbContext context, ILogger<News> logger) : base(context, logger)
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
                _logger.LogError(sqlEx, "SQL Error in CheckEntity with NewsId {NewsId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in CheckEntity with NewsId {NewsId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in CheckEntity with NewsId {NewsId}", id);
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
                _logger.LogError(sqlEx, "SQL Error in DeleteAsync for NewsId {NewsId}", t.NewsId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in DeleteAsync for NewsId {NewsId}", t.NewsId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in DeleteAsync for NewsId {NewsId}", t.NewsId);
                throw;
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
                News news = await _context.News.FirstOrDefaultAsync(news => news.NewsId == id);
                return news;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByIdAsync with NewsId {NewsId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByIdAsync with NewsId {NewsId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByIdAsync with NewsId {NewsId}", id);
                throw;
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
                _logger.LogError(sqlEx, "SQL Error in GetRandomNews");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetRandomNews");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetRandomNews");
                throw;
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
                _logger.LogError(sqlEx, "SQL Error in InsertAsync for NewsId {NewsId}", t.NewsId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in InsertAsync for NewsId {NewsId}", t.NewsId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in InsertAsync for NewsId {NewsId}", t.NewsId);
                throw;
            }
        }

        public override Task UpdateAsync(News t)
        {
            throw new NotImplementedException();
        }
    }
}
