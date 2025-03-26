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
    public abstract class AbstractUserPreferenceRepository : AbstractGenericBaseRepository<UserPreference>
    {
        protected AbstractUserPreferenceRepository(ApplicationDbContext context, ILogger<UserPreference> logger) : base(context, logger)
        {
        }

        public abstract Task<UserPreference> GetByUserIdAsync(int id);

    }
}
