using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Services
{
    public class PostService : AbstractPostService
    {
        private readonly AbstractPostRepository _postRepository;
        public PostService(AbstractPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
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
            _postRepository.Delete(t);
        }

        public override List<Post> TGetAll()
        {
            return _postRepository.GetAll();
        }

        public override Post TGetById(int id)
        {
            return _postRepository.GetById(id);
        }

        public override void TInsert(Post t)
        {
            _postRepository.Insert(t);
        }

        public override void TUpdate(Post t)
        {
            _postRepository.Update(t);
            //make changes
        }
    }
}
