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
    public abstract class AbstractBotRepository : AbstractGenericBaseRepository<Bot>
    {
        protected AbstractBotRepository(ApplicationDbContext context, ILogger<Bot> logger) : base(context, logger)
        {
        }
        public abstract Task<int> GetFollowerCountAsync(int id);
        public abstract Task<int> GetFollowedCountAsync(int id);
        public abstract Task<Bot> GetBotModuleAsync(int id);

    }
}
