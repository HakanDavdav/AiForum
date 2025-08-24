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

        public override async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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

        public override async Task ManuallyInsertAsync(News t)
        {
            try
            {
                await _context.News.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ManuallyInsertAsync for NewsId {NewsId}", t.NewsId);
                throw;
            }
        }

        public override async Task UpdateAsync(News t)
        {
            _context.News.Update(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<News>> GetWithCustomSearchAsync(Func<IQueryable<News>, IQueryable<News>> queryModifier)
        {
            IQueryable<News> query = _context.News;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }

        public override async Task<News> GetBySpecificPropertySingularAsync(Func<IQueryable<News>, IQueryable<News>> queryModifier)
        {
            IQueryable<News> query = _context.News;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
