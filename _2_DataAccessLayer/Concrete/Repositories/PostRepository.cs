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
    public class PostRepository : AbstractPostRepository
    {
        public PostRepository(ApplicationDbContext context, ILogger<Post> logger) : base(context, logger)
        {
        }

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Posts.AnyAsync(post => post.PostId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckEntity with PostId {PostId}", id);
                throw;
            }
        }

        public override async Task DeleteAsync(Post t)
        {
            try
            {
                _context.Posts.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for PostId {PostId}", t.PostId);
                throw;
            }
        }

        public override async Task<List<Post>> GetAllByBotIdAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                return await _context.Posts
                    .Where(post => post.BotId == id)
                    .OrderByDescending(post => post.DateTime)
                    .Skip(startInterval)
                    .Take(endInterval)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllByBotIdWithIntervalsAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Post>> GetAllByUserIdAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                return await _context.Posts
                    .Where(post => post.UserId == id)
                    .OrderByDescending(post => post.DateTime)
                    .Skip(startInterval)
                    .Take(endInterval)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllByUserIdWithIntervalsAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task<List<Post>> GetAllWithCustomSearch(Func<IQueryable<Post>, IQueryable<Post>> queryModifier)
        {
            try
            {
                var query = queryModifier(_context.Posts);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllWithCustomSearch");
                throw;
            }
        }

        public override async Task<Post> GetByEntryId(int id)
        {
            try
            {
                return await _context.Posts
                    .FirstOrDefaultAsync(post => post.Entries.Any(entry => entry.EntryId == id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByEntryId with EntryId {EntryId}", id);
                throw;
            }
        }

        public override async Task<Post> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Posts.FirstOrDefaultAsync(post => post.PostId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with PostId {PostId}", id);
                throw;
            }
        }

        public override async Task<Post> GetByTitleAsync(string title)
        {
            try
            {
                return await _context.Posts.FirstOrDefaultAsync(post => post.Title == title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByTitleAsync with Title {Title}", title);
                throw;
            }
        }

        public override async Task<int> GetPostCountByBotIdAsync(int id)
        {
            try
            {
                return await _context.Posts.CountAsync(post => post.BotId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPostCountByBotIdAsync with UserId {BotId}", id);
                throw;
            }
        }

        public override async Task<int> GetPostCountByUserIdAsync(int id)
        {
            try
            {
                return await _context.Posts.CountAsync(post => post.UserId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPostCountByUserIdAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task<List<Post>> GetRandomPosts(int number)
        {
            try
            {
                return await _context.Posts.OrderBy(post => Guid.NewGuid()).Take(number).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetRandomPosts");
                throw;
            }
        }

        public override async Task<List<Post>> GetRandomPostsByBotId(int id, int number)
        {
            try
            {
                return await _context.Posts
                    .Where(post => post.BotId == id)
                    .OrderBy(post => Guid.NewGuid())
                    .Take(number)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetRandomPostsByBotId with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Post>> GetRandomPostsByUserId(int id, int number)
        {
            try
            {
                return await _context.Posts
                    .Where(post => post.UserId == id)
                    .OrderBy(post => Guid.NewGuid())
                    .Take(number)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetRandomPostsByUserId with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task InsertAsync(Post t)
        {
            try
            {
                await _context.Posts.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InsertAsync for Post with Title {Title}", t.Title);
                throw;
            }
        }

        public override async Task UpdateAsync(Post t)
        {
            try
            {
                _context.Posts.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for PostId {PostId}", t.PostId);
                throw;
            }
        }
    }
}
