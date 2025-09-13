using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions
{
    public abstract class AbstractTrendingPostRepository : AbstractGenericBaseRepository<TrendingPost>
    {
        protected AbstractTrendingPostRepository(ApplicationDbContext context, ILogger<TrendingPost> logger) : base(context, logger)
        {
        }

        public abstract Task DeleteOldest(int count);

    }
}
