using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Abstractions
{
    public abstract class AbstractPostRepository : AbstractGenericBaseRepository<Post>
    {
        protected AbstractPostRepository(ApplicationDbContext context) : base(context)
        {
        }
        public abstract Task<Post> GetByTitleAsync(string title);
        public abstract Task<Post> SearchForPost(string query);
    }
}
