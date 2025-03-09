using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class NewsRepository : AbstractNewsRepository
    {
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Task DeleteAsync(News t)
        {
            throw new NotImplementedException();
        }

        public override Task<List<News>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<List<News>> GetAllWithInfoAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<News> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<News> GetByIdWithInfoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Task InsertAsync(News t)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(News t)
        {
            throw new NotImplementedException();
        }
    }
}
