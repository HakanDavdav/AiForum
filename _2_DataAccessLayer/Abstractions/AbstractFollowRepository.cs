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
    public abstract class AbstractFollowRepository : AbstractGenericBaseRepository<Follow>
    {
        protected AbstractFollowRepository(ApplicationDbContext context, ILogger<Follow> logger) : base(context, logger)
        {
        }


        public abstract Task<List<Follow>> GetFollowModulesForUserAsFollowedAsync(int id, int startInterval, int endInterval);
        public abstract Task<List<Follow>> GetFollowModulesForUserAsFollowerAsync(int id, int startInterval, int endInterval);

        public abstract Task<List<Follow>> GetFollowModulesForBotAsFollowedAsync(int id, int startInterval, int endInterval);
        public abstract Task<List<Follow>> GetFollowModulesForBotAsFollowerAsync(int id, int startInterval, int endInterval);

    }
}
