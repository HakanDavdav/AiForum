using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public abstract class AbstractGenericBaseRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger<T> _logger;
        protected AbstractGenericBaseRepository(ApplicationDbContext context, ILogger<T> logger)
        {
            _context = context;
            _logger = logger;
        }

        public abstract Task DeleteAsync(T t);
        public abstract Task<List<T>> GetWithCustomSearchAsync(Func<IQueryable<T>, IQueryable<T>> queryModifier);
        public abstract Task<T> GetByIdAsync(int id);
        public abstract Task ManuallyInsertAsync(T t);
        public abstract Task UpdateAsync(T t);
        public abstract Task<T> GetBySpecificPropertySingularAsync(Func<IQueryable<T>, IQueryable<T>> queryModifier);
        public abstract Task SaveChangesAsync();
        public abstract Task ManuallyInsertRangeAsync(List<T> t);
    }
}
