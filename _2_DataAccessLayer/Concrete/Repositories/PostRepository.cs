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

        public override void Delete(Post t)
        {
            _context.posts.Remove(t);
        }

        public override List<Post> GetAll()
        {
            IQueryable<Post> posts = _context.posts.Include(post => post.entries)
                .ThenInclude(entry => entry.user)
                .Include(post => post.likes);
            return posts.ToList();
        }

        public override Post GetById(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Post post = _context.posts.Include(post => post.entries)
                .ThenInclude(entry => entry.user)
                .Include(post => post.likes)
                .FirstOrDefault(post => post.postId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return post;
        }

        public override Post GetByTitle(string title)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Post post = _context.posts.Include(post => post.entries)
                .ThenInclude(entry => entry.user)
                .Include(post => post.likes)
                .FirstOrDefault(post => post.title == title);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return post;
        }

        public override void Insert(Post t)
        {
            _context.posts.Add(t);
        }

        public override void Update(Post t)
        {
            _context.posts.Attach(t);
            //make changes
        }
    }
}
