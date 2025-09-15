using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.Interfaces
{
    public abstract class AbstractFollowQueryHandler : AbstractGenericBaseQueryHandler<Follow> 
    {
        protected AbstractFollowQueryHandler(ILogger<Follow> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public abstract Task<List<Follow>> GetFollowModulesForBotAsFollowedAsync(int id, int startInterval, int endInterval);
        public abstract Task<List<Follow>> GetFollowModulesForBotAsFollowerAsync(int id, int startInterval, int endInterval);
        public abstract Task<List<Follow>> GetFollowModulesForUserAsFollowedAsync(int id, int startInterval, int endInterval);
        public abstract Task<List<Follow>> GetFollowModulesForUserAsFollowerAsync(int id, int startInterval, int endInterval);
    }
}