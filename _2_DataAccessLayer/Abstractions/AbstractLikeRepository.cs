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
    public abstract class AbstractLikeRepository : AbstractGenericBaseRepository<Like>
    {
        protected AbstractLikeRepository(ApplicationDbContext context) : base(context)
        {
        }
        public abstract Task<List<Like>> GetAllByUserIdAsync(int id);
        public abstract Task<List<Like>> GetAllByBotIdAsync(int id);

    }
}
