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
    public class FollowRepository : AbstractFollowRepository
    {
        public FollowRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(Follow t)
        {
            _context.follows.Remove(t);
            await _context.SaveChangesAsync();
        }


        public override async Task<List<Follow>> GetAllAsync()
        {
            IQueryable<Follow> allFollows = _context.follows;
            return await allFollows.ToListAsync();
        }

        public override async Task<List<Follow>> GetAllWithInfoAsync()
        {
            IQueryable<Follow> allFollows = _context.follows.Include(follow => follow.Followee)
                                                            .Include(follow => follow.Followed);
            return await allFollows.ToListAsync();
        }

        public override async Task<Follow> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Follow follow = await _context.follows.FirstOrDefaultAsync(follow => follow.FollowId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return follow;
        }

        public override async Task<Follow> GetByIdWithInfoAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Follow follow = await _context.follows.Include(follow => follow.Followee)
                                                  .Include(follow => follow.Followed)
                                                  .FirstOrDefaultAsync(follow => follow.FollowId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return follow;
        }

        public override async Task InsertAsync(Follow t)
        {
            await _context.follows.AddAsync(t);
            await _context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(Follow t)
        {
            _context.follows.Attach(t);
            await _context.SaveChangesAsync();
        }

    }
}
