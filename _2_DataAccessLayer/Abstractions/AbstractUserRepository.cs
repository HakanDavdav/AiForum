using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions
{
    public abstract class AbstractUserRepository : AbstractGenericBaseRepository<User>
    {
        protected AbstractUserRepository(ApplicationDbContext context, ILogger<User> logger) : base(context, logger)
        {
        }

        public abstract Task<User> GetUserModuleAsync(int id);
        public abstract Task<int> GetEntryCountAsync(int id);
        public abstract Task<int> GetPostCountAsync(int id);
        public abstract Task<int> GetNotificationCountAsync(int id);
        public abstract Task<int> GetBotActivitiesCountAsync(int id);
        public abstract Task<int> GetLikeCountAsync(int id);
        public abstract Task<int> GetFollowerCountAsync(int id);
        public abstract Task<int> GetFollowedCountAsync(int id);

    }
}
