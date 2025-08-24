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

        public override async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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

        public override async Task ManuallyInsertAsync(Like t)
        {
            try
            {
                await _context.Likes.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ManuallyInsertAsync for LikeId {LikeId}", t.LikeId);
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

        public override async Task<List<Like>> GetWithCustomSearchAsync(Func<IQueryable<Like>, IQueryable<Like>> queryModifier)
        {
            IQueryable<Like> query = _context.Likes;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }

        public override async Task<Like> GetBySpecificPropertySingularAsync(Func<IQueryable<Like>, IQueryable<Like>> queryModifier)
        {
            IQueryable<Like> query = _context.Likes;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task<List<Like>> GetLikeModulesForUser(int userId, int startInterval, int endInterval)
        {
            var likes = await _context.Likes
                .Where(like => like.UserId == userId)
                .OrderByDescending(like => like.DateTime)
                .Skip(startInterval)
                .Take(endInterval - startInterval)
                .Select(like => new Like
                {
                    LikeId = like.LikeId,
                    DateTime = like.DateTime,
                    UserId = like.UserId,
                    BotId = like.BotId,
                    PostId = like.PostId,
                    User = like.User,
                    Bot = like.Bot,
                    Post = like.Post,
                }).ToListAsync();
            return likes;
        }

        public override async Task<List<Like>> GetLikeModulesForBot(int botId, int startInterval, int endInterval)
        {
            var likes = await _context.Likes
                .Where(like => like.BotId == botId)
                .OrderByDescending(like => like.DateTime)
                .Skip(startInterval)
                .Take(endInterval - startInterval)
                .Select(like => new Like
                {
                    LikeId = like.LikeId,
                    DateTime = like.DateTime,
                    UserId = like.UserId,
                    BotId = like.BotId,
                    PostId = like.PostId,
                    User = like.User,
                    Bot = like.Bot,
                    Post = like.Post,
                }).ToListAsync();
            return likes;
        }

        public override async Task<List<Like>> GetLikeModulesForEntry(int entryId, int startInterval, int endInterval)
        {
            var likes = await _context.Likes
                .Where(like => like.EntryId == entryId)
                .OrderByDescending(like => like.DateTime)
                .Skip(startInterval)
                .Take(endInterval - startInterval)
                .Select(like => new Like
                {
                    LikeId = like.LikeId,
                    DateTime = like.DateTime,
                    UserId = like.UserId,
                    BotId = like.BotId,
                    PostId = like.PostId,
                    User = like.User,
                    Bot = like.Bot,
                    Post = like.Post,
                }).ToListAsync();
            return likes;
        }

        public override async Task<List<Like>> GetLikeModulesForPost(int postId, int startInterval, int endInterval)
        {
            var likes = await _context.Likes
                .Where(like => like.PostId == postId)
                .OrderByDescending(like => like.DateTime)
                .Skip(startInterval)
                .Take(endInterval - startInterval)
                .Select(like => new Like
                {
                    LikeId = like.LikeId,
                    DateTime = like.DateTime,
                    UserId = like.UserId,
                    BotId = like.BotId,
                    PostId = like.PostId,
                    User = like.User,
                    Bot = like.Bot,
                    Post = like.Post,
                }).ToListAsync();
            return likes;
        }
    }
}
