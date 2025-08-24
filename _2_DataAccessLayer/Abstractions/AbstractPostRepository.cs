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
    public abstract class AbstractPostRepository : AbstractGenericBaseRepository<Post>
    {
        protected AbstractPostRepository(ApplicationDbContext context, ILogger<Post> logger) : base(context, logger)
        {
        }
        public abstract Task<List<Post>> GetPostModulesForUser(int id,int startInterval, int endInterval);
        public abstract Task<List<Post>> GetPostModulesForBot(int id,int startInterval, int endInterval);
        public abstract Task<Post> GetPostModuleAsync(int id);
        public abstract Task<int> GetEntryCountOfPost(int id);
        public abstract Task<int> GetLikeCountOfPost(int id);
    }
}
