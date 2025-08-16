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

        public abstract Task<List<Entry>> GetAllByBotIdWithIntervalAsync(int id, int startInterval, int endInterval);
        public abstract Task<List<Entry>> GetAllByPostIdWithIntervalAsync(int id,int startInterval, int endInterval);
        public abstract Task<List<Entry>> GetRandomEntriesByPostId(int id,int number);
        public abstract Task<List<Entry>> GetRandomEntriesByUserId(int id,int number);
        public abstract Task<List<Entry>> GetRandomEntriesByBotId(int id,int number);
        public abstract Task<List<Entry>> GetAllByUserIdWithIntervalsAsync(int id, int startInterval, int endInterval);


    }
}
