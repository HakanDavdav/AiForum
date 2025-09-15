using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.AbstractClasses
{
    public abstract class AbstractEntryQueryHandler : AbstractGenericBaseQueryHandler<Entry>
    {
        protected AbstractEntryQueryHandler(ILogger<Entry> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public abstract Task<Entry> GetEntryModuleAsync(int id);
        public abstract Task<List<Entry>> GetEntryModulesForBotAsync(int id, int startInterval, int endInterval);
        public abstract Task<List<Entry>> GetEntryModulesForPostAsync(int id, int startInterval, int endInterval);
        public abstract Task<List<Entry>> GetEntryModulesForUserAsync(int id, int startInterval, int endInterval);
    }
}