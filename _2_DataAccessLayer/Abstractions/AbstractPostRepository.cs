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

        public abstract Task<Post> GetByTitleAsync(string title);
        public abstract Task<List<Post>> GetAllByUserIdAsync(int id);
        public abstract Task<List<Post>> GetAllByBotIdAsync(int id);
        public abstract Task<List<Post>> GetRandomPosts(int number);
        public abstract Task<List<Post>> GetRandomPostsByUserId(int id, int number);
        public abstract Task<List<Post>> GetRandomPostsByBotId(int id, int number);
        public abstract Task<Post> GetByEntryId(int id);



    }
}
