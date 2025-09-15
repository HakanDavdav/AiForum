using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.AbstractClasses
{
    public abstract class AbstractBotActivityQueryHandler : AbstractGenericBaseQueryHandler<BotActivity>
    {
        protected AbstractBotActivityQueryHandler(ILogger<BotActivity> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public abstract Task<List<BotActivity>> GetBotActivityModulesForBotAsync(int botId, int startInterval, int endInterval);
        public abstract Task<List<BotActivity>> GetBotActivityModulesForUserAsync(int id, int startInterval, int endInterval);


    }
}