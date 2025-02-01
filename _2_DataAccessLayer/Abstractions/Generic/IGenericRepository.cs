using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public interface IGenericRepository<T> 
    {
        public void Insert(T t);
        public void Delete(T t);
        public void Update(T t);
        public List<T> GetAll();
        public T GetById(int id);
    }
}
