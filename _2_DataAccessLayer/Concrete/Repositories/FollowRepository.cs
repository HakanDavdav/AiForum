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

        public override async Task<List<Follow>> GetAllByBotIdAsync(int id)
        {
            var follows = _context.Follows.Where(follow => follow.BotFolloweeId == id);
            return await follows.ToListAsync();
        }

        public override async Task<List<Follow>> GetAllByUserIdAsync(int id)
        {
            var follows = _context.Follows.Where(follow => follow.FolloweeId == id);
            return await follows.ToListAsync();
        }


        public override async Task<Follow> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Follow follow = await _context.Follows.FirstOrDefaultAsync(follow => follow.FollowId == id);
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
