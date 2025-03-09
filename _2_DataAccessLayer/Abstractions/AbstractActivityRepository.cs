using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Abstractions
{
    public class AbstractActivityRepository : IGenericRepository<Activity>
    {
        public AbstractActivityRepository()
        {
        }

        public Task DeleteAsync(Activity t)
        {
            
        }

        public Task<List<Activity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Activity>> GetAllWithInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Activity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Activity> GetByIdWithInfoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Activity t)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Activity t)
        {
            throw new NotImplementedException();
        }
    }
}
