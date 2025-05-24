using System;
using System.Collections.Generic;
using System.Linq;
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckEntity with NewsId {NewsId}", id);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for NewsId {NewsId}", t.NewsId);
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
                return await _context.News.FirstOrDefaultAsync(news => news.NewsId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with NewsId {NewsId}", id);
                throw;
            }
        }

        public override async Task<List<News>> GetRandomNews(int number)
        {
            try
            {
                var query = _context.News.OrderBy(n => Guid.NewGuid()).Take(number);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetRandomNews");
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InsertAsync for NewsId {NewsId}", t.NewsId);
                throw;
            }
        }

        public override Task UpdateAsync(News t)
        {
            throw new NotImplementedException();
        }
    }
}
