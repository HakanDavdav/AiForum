using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.AbstractClasses
{
    public abstract class AbstractLikeQueryHandler : AbstractGenericBaseQueryHandler<Like> 
    {
        protected AbstractLikeQueryHandler(ILogger<Like> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public abstract Task<List<Like>> GetLikeModulesForBotAsync(int botId, int startInterval, int endInterval);
        public abstract Task<List<Like>> GetLikeModulesForEntryAsync(int entryId, int startInterval, int endInterval);
        public abstract Task<List<Like>> GetLikeModulesForPostAsync(int postId, int startInterval, int endInterval);
        public abstract Task<List<Like>> GetLikeModulesForUserAsync(int userId, int startInterval, int endInterval);
    }
}