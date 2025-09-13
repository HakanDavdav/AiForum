using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class TrendingPostRepository : AbstractTrendingPostRepository
    {
        public TrendingPostRepository(ApplicationDbContext context, ILogger<TrendingPost> logger) : base(context, logger)
        {
        }



        public override async Task<List<TrendingPost>> GetWithCustomSearchAsync(Func<IQueryable<TrendingPost>, IQueryable<TrendingPost>> queryModifier)
        {
            IQueryable<TrendingPost> query = _context.TrendingPosts;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }

        public override async Task<TrendingPost> GetBySpecificPropertySingularAsync(Func<IQueryable<TrendingPost>, IQueryable<TrendingPost>> queryModifier)
        {
            IQueryable<TrendingPost> query = _context.TrendingPosts;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override Task DeleteOldest(int count)
        {
            throw new NotImplementedException();
        }
    }
}
