using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions
{
    public abstract class AbstractEntryRepository : AbstractGenericBaseRepository<Entry>
    {
        protected AbstractEntryRepository(ApplicationDbContext context, ILogger<Entry> logger) : base(context, logger)
        {
        }
        public abstract Task<List<Entry>> GetEntryModulesForUserAsync(int id, int startInterval, int endInterval);
        public abstract Task<List<Entry>> GetEntryModulesForBotAsync(int id, int startInterval, int endInterval);
        public abstract Task<List<Entry>> GetEntryModulesForPostAsync(int id,int startInterval, int endInterval);
        public abstract Task<Entry> GetEntryModuleAsync(int id);



    }
}
