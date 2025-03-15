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


        public override async Task DeleteAsync(Post t)
        {

            _context.Posts.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<Post>> GetAllByBotIdAsync(int id)
        {
            IQueryable<Post> posts = _context.Posts.Where(post => post.BotId == id);
            return await posts.ToListAsync();

        }

        public override async Task<List<Post>> GetAllByUserIdAsync(int id)
        {
            IQueryable<Post> posts = _context.Posts.Where(post =>post.UserId == id);
            return await posts.ToListAsync();
        }

        public override Task<Post> GetByEntryId(int id)
        {
            var post = _context.Posts
                        .FirstOrDefaultAsync(post => post.Entries.Any(entry => entry.EntryId == id));
            return post;
        }

        public override async Task<Post> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Post post = await _context.Posts.FirstOrDefaultAsync(post => post.PostId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return post;
        }



        public override async Task<Post> GetByTitleAsync(string title)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Post post = await _context.Posts.FirstOrDefaultAsync(post => post.Title == title);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return post;
        }

        public override async Task<List<Post>> GetRandomPosts(int number)
        {
            IQueryable<Post> posts = _context.Posts.OrderBy(post => Guid.NewGuid()).Take(number);
            return await posts.ToListAsync();
        }

        public override async Task<List<Post>> GetRandomPostsByBotId(int id, int number)
        {
            IQueryable<Post> posts = _context.Posts.Where(post =>post.BotId == id).OrderBy(post => Guid.NewGuid()).Take(number);
            return await posts.ToListAsync();
        }

        public override async Task<List<Post>> GetRandomPostsByUserId(int id, int number)
        {
            IQueryable<Post> posts = _context.Posts.Where(post => post.UserId == id).OrderBy(post => Guid.NewGuid()).Take(number);
            return await posts.ToListAsync();
        }

        public override async Task InsertAsync(Post t)
        {
            await _context.Posts.AddAsync(t);
            await _context.SaveChangesAsync();
        }


        public override async Task UpdateAsync(Post t)
        {
            _context.Update(t);
            await _context.SaveChangesAsync();

        }
        
    }
}
