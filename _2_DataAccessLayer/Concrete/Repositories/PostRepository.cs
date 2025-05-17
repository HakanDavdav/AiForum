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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in CheckEntity with PostId {PostId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in CheckEntity with PostId {PostId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in CheckEntity with PostId {PostId}", id);
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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in DeleteAsync for PostId {PostId}", t.PostId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in DeleteAsync for PostId {PostId}", t.PostId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in DeleteAsync for PostId {PostId}", t.PostId);
                throw;
            }
        }

        public override async Task<List<Post>> GetAllByBotIdAsync(int id)
        {
            try
            {
                IQueryable<Post> posts = _context.Posts.Where(post => post.BotId == id);
                return await posts.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetAllByBotIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetAllByBotIdAsync with BotId {BotId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetAllByBotIdAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Post>> GetAllByUserIdAsync(int id,int startInterval, int endInterval)
        {
            try
            {
                IQueryable<Post> posts = _context.Posts.Where(post => post.UserId == id).OrderByDescending(post => post.DateTime).Skip(startInterval).Take(endInterval);
                return await posts.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetAllByUserIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetAllByUserIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetAllByUserIdAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task<List<Post>> GetAllWithCustomSearch(Func<IQueryable<Post>, IQueryable<Post>> queryModifier)
        {
            try
            {
                IQueryable<Post> query = _context.Posts;
                query = queryModifier(query);
                return await query.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetAllWithCustomSearch");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetAllWithCustomSearch");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetAllWithCustomSearch");
                throw;
            }
        }

        public override async Task<Post> GetByEntryId(int id)
        {
            try
            {
                var post = await _context.Posts
                        .FirstOrDefaultAsync(post => post.Entries.Any(entry => entry.EntryId == id));
                return post;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByEntryId with EntryId {EntryId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByEntryId with EntryId {EntryId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByEntryId with EntryId {EntryId}", id);
                throw;
            }
        }

        public override async Task<Post> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Posts.FirstOrDefaultAsync(post => post.PostId == id);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByIdAsync with PostId {PostId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByIdAsync with PostId {PostId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByIdAsync with PostId {PostId}", id);
                throw;
            }
        }

        public override async Task<Post> GetByTitleAsync(string title)
        {
            try
            {
                return await _context.Posts.FirstOrDefaultAsync(post => post.Title == title);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByTitleAsync with Title {Title}", title);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByTitleAsync with Title {Title}", title);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByTitleAsync with Title {Title}", title);
                throw;
            }
        }

        public override async Task<List<Post>> GetRandomPosts(int number)
        {
            try
            {
                IQueryable<Post> posts = _context.Posts.OrderBy(post => Guid.NewGuid()).Take(number);
                return await posts.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetRandomPosts");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetRandomPosts");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetRandomPosts");
                throw;
            }
        }

        public override async Task<List<Post>> GetRandomPostsByBotId(int id, int number)
        {
            try
            {
                IQueryable<Post> posts = _context.Posts.Where(post => post.BotId == id).OrderBy(post => Guid.NewGuid()).Take(number);
                return await posts.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetRandomPostsByBotId with BotId {BotId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetRandomPostsByBotId with BotId {BotId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetRandomPostsByBotId with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Post>> GetRandomPostsByUserId(int id, int number)
        {
            try
            {
                IQueryable<Post> posts = _context.Posts.Where(post => post.UserId == id).OrderBy(post => Guid.NewGuid()).Take(number);
                return await posts.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetRandomPostsByUserId with UserId {UserId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetRandomPostsByUserId with UserId {UserId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetRandomPostsByUserId with UserId {UserId}", id);
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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in InsertAsync for PostId {PostId}", t.PostId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in InsertAsync for PostId {PostId}", t.PostId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in InsertAsync for PostId {PostId}", t.PostId);
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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in UpdateAsync for PostId {PostId}", t.PostId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in UpdateAsync for PostId {PostId}", t.PostId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in UpdateAsync for PostId {PostId}", t.PostId);
                throw;
            }
        }
    }
}
