using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class LikeRepository : AbstractLikeRepository
    {
        public LikeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override void Delete(Like t)
        {
            _context.likes.Remove(t);
        }

        public override List<Like> GetAll()
        {
            IQueryable<Like> likes = _context.likes;
            return likes.ToList();
        }

        public override Like GetById(int id)
        {
            Like like = _context.likes.Find(id);
            return like;
        }

        public override void Insert(Like t)
        {
            _context.likes.Add(t);
        }

        public override void Update(Like t)
        {
            _context.likes.Attach(t);
            //make changes
        }
    }
}
