using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _2_DataAccessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices
{
    public abstract class AbstractPostService : IPostService
    {
        protected readonly AbstractPostRepository _postRepository;
        protected readonly AbstractLikeRepository _likeRepository;
        protected AbstractPostService(AbstractLikeRepository likeRepository, AbstractPostRepository postRepository)
        {
            _postRepository = postRepository;
            _likeRepository = likeRepository;
        }
        public abstract Task<IdentityResult> CreateComplaint(int userId, int postId);
        public abstract Task<IdentityResult> CreatePost(int userId, string title, string context);
        public abstract Task<IdentityResult> DeletePost(int userId, int postId);
        public abstract Task<IdentityResult> LikePost(int userId, int postId);
        public abstract Task<IdentityResult> UnlikePost(int userId, int postId);
        public abstract Task<IdentityResult> UpdatePost(int userId, int postId, string title, string context);
    }
}
