using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public abstract class AbstractGenericBaseRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected AbstractGenericBaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public abstract Task DeleteAsync(T t);
        public abstract Task<List<T>> GetAllAsync();
        public abstract Task<List<T>> GetAllWithInfoAsync();
        public abstract Task<T> GetByIdAsync(int id);
        public abstract Task<T> GetByIdWithInfoAsync(int id);
        public abstract Task InsertAsync(T t);
        public abstract Task UpdateAsync(T t);
    }
}
