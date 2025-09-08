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
        public NewsRepository(ApplicationDbContext context, ILogger<TrendingPosts> logger) : base(context, logger)
        {
        }

        public override async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public override async Task DeleteAsync(TrendingPosts t)
        {
            try
            {
                _context.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for TrendingPostId {TrendingPostId}", t.TrendingPostId);
                throw;
            }
        }


        public override async Task<TrendingPosts> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null) return null;
                return await _context.News.FirstOrDefaultAsync(news => news.TrendingPostId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with TrendingPostId {TrendingPostId}", id);
                throw;
            }
        }
        public override async Task ManuallyInsertAsync(TrendingPosts t)
        {
            try
            {
                await _context.News.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ManuallyInsertAsync for TrendingPostId {TrendingPostId}", t.TrendingPostId);
                throw;
            }
        }

        public override async Task UpdateAsync(TrendingPosts t)
        {
            _context.News.Update(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<TrendingPosts>> GetWithCustomSearchAsync(Func<IQueryable<TrendingPosts>, IQueryable<TrendingPosts>> queryModifier)
        {
            IQueryable<TrendingPosts> query = _context.News;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }

        public override async Task<TrendingPosts> GetBySpecificPropertySingularAsync(Func<IQueryable<TrendingPosts>, IQueryable<TrendingPosts>> queryModifier)
        {
            IQueryable<TrendingPosts> query = _context.News;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task ManuallyInsertRangeAsync(List<TrendingPosts> trendingPosts)
        {
            _context.News.AddRange(trendingPosts);
            await _context.SaveChangesAsync();
        }
    }
}
