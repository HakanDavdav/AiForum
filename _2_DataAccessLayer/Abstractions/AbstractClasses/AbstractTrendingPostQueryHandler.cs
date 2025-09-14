using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.AbstractClasses
{
    public class AbstractTrendingPostQueryHandler : AbstractGenericBaseQueryHandler<TrendingPost>
    {
        public AbstractTrendingPostQueryHandler(ILogger<TrendingPost> logger, AbstractGenericBaseCommandHandler repository) : base(logger, repository)
        {
        }
    }
}
