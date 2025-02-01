using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions.Generic
{
    public interface IGenericService<T> 
    {
        public void TInsert(T t);
        public void TDelete(T t);
        public void TUpdate(T t);
        public List<T> TGetAll();
        public T TGetById(int id);
    }
}
