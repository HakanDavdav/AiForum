using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.MainServices;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Services.MainServices
{
    public class PostService : AbstractPostService
    {
        public override void CreatePost(string text, int userId, string title)
        {
            throw new NotImplementedException();
        }

        public override void EditPost(string text, int userId)
        {
            throw new NotImplementedException();
        }

        public override void TDelete(Post t)
        {
            throw new NotImplementedException();
        }

        public override List<Post> TGetAll()
        {
            throw new NotImplementedException();
        }

        public override Post TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void TInsert(Post t)
        {
            throw new NotImplementedException();
        }

        public override void TUpdate(Post t)
        {
            throw new NotImplementedException();
        }
    }
}
