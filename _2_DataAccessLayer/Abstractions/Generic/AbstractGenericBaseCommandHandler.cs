using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public abstract class AbstractGenericBaseCommandHandler
    {
        protected readonly ApplicationDbContext _context;
        protected AbstractGenericBaseCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public virtual IQueryable<T> Export<T>() where T : class
        {
            return _context.Set<T>().AsQueryable();
        }

        public virtual async Task DeleteAsync<T>(T t) where T : class
        {
            _context.Set<T>().Remove(t);
        }

        public virtual async Task DeleteRangeAsync<T>(List<T> t) where T : class
        {
            _context.Set<T>().RemoveRange(t);
        }

        public virtual async Task ManuallyInsertAsync<T>(T t) where T : class
        {
            await _context.Set<T>().AddAsync(t);
        }

        public virtual async Task ManuallyInsertRangeAsync<T>(List<T> t) where T : class
        {
            await _context.Set<T>().AddRangeAsync(t);
        }

        public virtual async Task SaveChangesAsync() 
        {
            await _context.SaveChangesAsync();
        }

    }
}
