using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.Interfaces
{
    public abstract class AbstractNotificationQueryHandler : AbstractGenericBaseQueryHandler<Notification> 
    {
        protected AbstractNotificationQueryHandler(ILogger<Notification> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public abstract Task<List<Notification>> GetNotificationModulesForUserAsync(int id, int startInterval, int endInterval);
    }
}