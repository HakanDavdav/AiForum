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
    public class PostRepository : AbstractPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
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
        public override async Task DeleteAsync(Post t)
        {
            try
            {
                _context.Posts.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in DeleteAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in DeleteAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in DeleteAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
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
                Console.WriteLine($"SQL Error in GetAllByBotIdAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetAllByBotIdAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetAllByBotIdAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
            }
        }

        public override async Task<List<Post>> GetAllByUserIdAsync(int id)
        {
            try
            {
                IQueryable<Post> posts = _context.Posts.Where(post => post.UserId == id);
                return await posts.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetAllByUserIdAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetAllByUserIdAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetAllByUserIdAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
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
                Console.WriteLine($"SQL Error in GetByEntryId: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetByEntryId: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetByEntryId: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
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
                Console.WriteLine($"SQL Error in GetByIdAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetByIdAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetByIdAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
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
                Console.WriteLine($"SQL Error in GetByTitleAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetByTitleAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetByTitleAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
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
                Console.WriteLine($"SQL Error in GetRandomPosts: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetRandomPosts: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetRandomPosts: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
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
                Console.WriteLine($"SQL Error in GetRandomPostsByBotId: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetRandomPostsByBotId: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetRandomPostsByBotId: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
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
                Console.WriteLine($"SQL Error in GetRandomPostsByUserId: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetRandomPostsByUserId: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetRandomPostsByUserId: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
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
                Console.WriteLine($"SQL Error in InsertAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in InsertAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in InsertAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
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
                Console.WriteLine($"SQL Error in UpdateAsync: {sqlEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in UpdateAsync: {invalidOpEx.Message}");
                throw; // Rethrow the caught exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in UpdateAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the caught exception
            }
        }
    }
}
