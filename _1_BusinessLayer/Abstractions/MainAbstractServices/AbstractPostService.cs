using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions.MainServices
{
    public abstract class AbstractPostService
    {
        public abstract void CreatePostAsync(string text, int userId, string title);
        public abstract void EditPostAsync(string text, int userId);
    }
}
