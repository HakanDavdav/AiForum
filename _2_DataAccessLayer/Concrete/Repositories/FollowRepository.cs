using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class FollowRepository : AbstractFollowRepository
    {
        public FollowRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override void Delete(Follow t)
        {
            _context.follows.Remove(t);
        }

        public override List<Follow> GetAll()
        {
            IQueryable<Follow> follows = _context.follows;
            return follows.ToList();
        }

        public override Follow GetById(int id)
        {
            Follow follow = _context.follows.Find(id);
            return follow;
        }

        public override void Insert(Follow t)
        {
            _context.follows.Add(t);
        }

        public override void Update(Follow t)
        {
             _context.follows.Attach(t);
             //make changes
        }
    }
}
