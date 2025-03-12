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
            _context.Follows.Remove(t);
            await _context.SaveChangesAsync();
        }


        public override async Task<IQueryable<Follow>> GetAllAsync()
        {
            IQueryable<Follow> allFollows = _context.Follows;
            return allFollows;
        }

        public override async Task<IQueryable<Follow>> GetAllWithInfoAsync()
        {
            IQueryable<Follow> allFollows = _context.Follows.Include(follow => follow.Followee)
                                                            .Include(follow => follow.Followed)
                                                            .Include(follow => follow.BotFollowee)
                                                            .Include(follow => follow.BotFollowed);
            return allFollows;
        }

        public override async Task<Follow> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Follow follow = await _context.Follows.FirstOrDefaultAsync(follow => follow.FollowId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return follow;
        }

        public override async Task<Follow> GetByIdWithInfoAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Follow follow = await _context.Follows.Include(follow => follow.Followee)
                                                            .Include(follow => follow.Followed)
                                                            .Include(follow => follow.BotFollowee)
                                                            .Include(follow => follow.BotFollowed)
                                                            .FirstOrDefaultAsync(follow => follow.FollowId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return follow;
        }

        public override async Task InsertAsync(Follow t)
        {
            await _context.Follows.AddAsync(t);
            await _context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(Follow t)
        {
            _context.Update(t);
            await _context.SaveChangesAsync();
        }

    }
}
