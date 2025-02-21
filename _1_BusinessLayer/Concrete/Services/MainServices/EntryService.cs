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
        public override void CreateEntryAsync(string text, int userId, string title)
        {
            throw new NotImplementedException();
        }

        public override void EditEntryAsync(string text, int userId)
        {
            throw new NotImplementedException();
        }

       
    }
}
