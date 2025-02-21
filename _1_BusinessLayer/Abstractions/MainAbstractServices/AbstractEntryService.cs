using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions.MainServices
{
    public abstract class AbstractEntryService 
    {
        public abstract void CreateEntryAsync(string text, int userId, string title);
        public abstract void EditEntryAsync(string text, int userId);
    }
}
