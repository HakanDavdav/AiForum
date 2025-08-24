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
    public class LikeRepository : AbstractLikeRepository
    {
        public LikeRepository(ApplicationDbContext context, ILogger<Like> logger) : base(context, logger)
        {
        }

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Likes.AnyAsync(like => like.LikeId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckEntity with LikeId {LikeId}", id);
                throw;
            }
        }

        public override async Task DeleteAsync(Like t)
        {
            try
            {
                _context.Likes.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for LikeId {LikeId}", t.LikeId);
                throw;
            }
        }

        public override async Task<List<Like>> GetWithCustomSearchAsync(Func<IQueryable<Like>, IQueryable<Like>> queryModifier)
        {
            IQueryable<Like> query = _context.Likes;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }
        public override async Task<Like> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Likes.FirstOrDefaultAsync(like => like.LikeId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with LikeId {LikeId}", id);
                throw;
            }
        }

        public override async Task InsertAsync(Like t)
        {
            try
            {
                await _context.Likes.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InsertAsync for LikeId {LikeId}", t.LikeId);
                throw;
            }
        }

        public override async Task UpdateAsync(Like t)
        {
            try
            {
                _context.Likes.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for LikeId {LikeId}", t.LikeId);
                throw;
            }
        }

        public override Task<List<Like>> GetBySpecificProperty(Func<IQueryable<Like>, IQueryable<Like>> queryModifier)
        {
            throw new NotImplementedException();
        }
    }
}
