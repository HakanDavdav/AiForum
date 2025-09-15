using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.Interfaces
{
    public abstract class AbstractUserQueryHandler : AbstractGenericBaseQueryHandler<User>
    {
        protected AbstractUserQueryHandler(ILogger<User> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public abstract Task<User> GetUserModuleAsync(int id);
        public abstract Task<User> GetUserWithBotTreeAsync(int id);
    }
}