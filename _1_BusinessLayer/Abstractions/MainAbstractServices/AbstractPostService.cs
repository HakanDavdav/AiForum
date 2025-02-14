using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions.MainServices
{
    public abstract class AbstractPostService : EntityAbstractGenericBaseService<Post>
    {
        public abstract void CreatePost(string text, int userId, string title);
        public abstract void EditPost(string text, int userId);
    }
}
