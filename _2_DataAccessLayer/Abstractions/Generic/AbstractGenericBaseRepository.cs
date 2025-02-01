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

        public abstract void Delete(T t);
        public abstract List<T> GetAll();
        public abstract T GetById(int id);
        public abstract void Insert(T t);
        public abstract void Update(T t);
    }
}
