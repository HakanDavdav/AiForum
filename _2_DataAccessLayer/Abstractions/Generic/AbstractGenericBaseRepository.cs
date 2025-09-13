using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public virtual async Task DeleteAsync(T t)
        {
            _context.Set<T>().Remove(t);
            await _context.SaveChangesAsync();
        }
        public virtual async Task<List<T>> GetWithCustomSearchAsync(Func<IQueryable<T>, IQueryable<T>> queryModifier)
        {
            IQueryable<T> query = _context.Set<T>();
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }
        public virtual async Task<T> GetByIdAsync(int? id)
        {
            if (id == null) return null;
            return await _context.Set<T>().FindAsync(id);
        }
        public virtual async Task ManuallyInsertAsync(T t)
        {
            _context.Set<T>().Add(t);
            await _context.SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(T t)
        {
            _context.Set<T>().Update(t);
            await _context.SaveChangesAsync();
        }
        public virtual async Task<T> GetBySpecificPropertySingularAsync(Func<IQueryable<T>, IQueryable<T>> queryModifier)
        {
            IQueryable<T> query = _context.Set<T>();
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public virtual async Task ManuallyInsertRangeAsync(List<T> t)
        {
            await _context.Set<T>().AddRangeAsync(t);
            await _context.SaveChangesAsync();
        }
        public virtual async Task StartTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public virtual async Task CommitTransaction()
        {
            await _context.Database.CommitTransactionAsync();
        }
        public virtual async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
