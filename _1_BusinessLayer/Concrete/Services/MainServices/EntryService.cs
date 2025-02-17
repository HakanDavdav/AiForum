using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.MainServices;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Services.MainServices
{
    public class EntryService : AbstractEntryService
    {
        public override void TDelete(Entry t)
        {
            throw new NotImplementedException();
        }

        public override List<Entry> TGetAll()
        {
            throw new NotImplementedException();
        }

        public override Entry TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void TInsert(Entry t)
        {
            throw new NotImplementedException();
        }

        public override void TUpdate(Entry t)
        {
            throw new NotImplementedException();
        }
    }
}
