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
    public abstract class AbstractBotRepository : AbstractGenericBaseRepository<Bot>
    {
        protected AbstractBotRepository(ApplicationDbContext context, ILogger<Bot> logger) : base(context, logger)
        {
        }

        public abstract Task<List<Bot>> GetAllByUserIdAsync(int id);
        public abstract Task<List<Bot>> GetRandomBots(int number);
        public abstract Task<int> GetEntryCountOfBotAsync(int number);
        public abstract Task<int> GetPostCountOfBotAsync(int number);

    }
}
