using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;

namespace _1_BusinessLayer.Abstractions.Generic
{
    public abstract class AbstractGenericBaseService<T> : IGenericService<T> where T : class
    {
       
        protected AbstractGenericBaseService()
        {
            
        }

        public abstract void TDelete(T t);
        public abstract List<T> TGetAll();
        public abstract T TGetById(int id);
        public abstract void TInsert(T t);
        public abstract void TUpdate(T t);
    }
}
