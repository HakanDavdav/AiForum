using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

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
            IQueryable<Post> posts = _context.posts;
            return posts.ToList();
        }

        public override Post GetById(int id)
        {
            Post post = _context.posts.Find(id);
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
