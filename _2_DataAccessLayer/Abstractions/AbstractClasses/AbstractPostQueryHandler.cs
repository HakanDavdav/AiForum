using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.AbstractClasses
{
    public abstract class AbstractPostQueryHandler : AbstractGenericBaseQueryHandler<Post> 
    {
        protected AbstractPostQueryHandler(ILogger<Post> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public abstract Task<Post> GetPostModuleAsync(int id);
        public abstract Task<List<Post>> GetPostModulesForBotAsync(int id, int startInterval, int endInterval);
        public abstract Task<List<Post>> GetPostModulesForUserAsync(int id, int startInterval, int endInterval);
    }
}