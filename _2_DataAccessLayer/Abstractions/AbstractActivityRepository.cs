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
    public abstract class AbstractActivityRepository : AbstractGenericBaseRepository<BotActivity>
    {
        protected AbstractActivityRepository(ApplicationDbContext context, ILogger<BotActivity> logger) : base(context, logger)
        {
        }
        public abstract Task<List<BotActivity>> GetBotActivityModulesForBotAsync(int botId, int startInterval, int endInterval );
        public abstract Task<List<BotActivity>> GetBotActivityModulesForUserAsync(int botId, int startInterval, int endInterval);

    }
}
