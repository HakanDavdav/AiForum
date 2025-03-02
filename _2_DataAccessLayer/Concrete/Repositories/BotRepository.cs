using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class BotRepository : AbstractBotRepository
    {
        public BotRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Task DeleteAsync(Bot t)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Bot>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<Bot> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Task InsertAsync(Bot t)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(Bot t)
        {
            throw new NotImplementedException();
        }
    }
}
