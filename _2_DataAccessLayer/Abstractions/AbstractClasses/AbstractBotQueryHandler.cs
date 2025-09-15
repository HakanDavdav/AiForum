using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.AbstractClasses
{
    public abstract class AbstractBotQueryHandler : AbstractGenericBaseQueryHandler<Bot>
    {
        protected AbstractBotQueryHandler(ILogger<Bot> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public abstract Task<Bot> GetBotModuleAsync(int id);
        public abstract Task<Bot> GetBotWithChildBotTreeAsync(int id);
    }
}