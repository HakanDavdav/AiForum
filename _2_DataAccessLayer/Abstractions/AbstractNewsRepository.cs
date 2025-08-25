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
    public abstract class AbstractNewsRepository : AbstractGenericBaseRepository<TrendingPosts>
    {
        protected AbstractNewsRepository(ApplicationDbContext context, ILogger<TrendingPosts> logger) : base(context, logger)
        {
        }

    }
}
