using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public abstract class AbstractGenericCommandHandler
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger _logger;

        protected AbstractGenericCommandHandler(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }


        public abstract Task ManuallyInsertAsync<T>(T entity) where T : class;
        public abstract Task ManuallyInsertRangeAsync<T>(List<T> entities) where T : class;
        public abstract Task DeleteAsync<T>(T entity) where T : class;
        public abstract Task DeleteRangeAsync<T>(List<T> entities) where T : class;
        public abstract Task SaveChangesAsync();
    }
}
