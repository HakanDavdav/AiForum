using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices
{
    public abstract class AbstractPostService : IPostService
    {
        protected readonly AbstractPostRepository _postRepository;
        protected readonly AbstractUserRepository _userRepository;

        protected AbstractPostService(AbstractPostRepository postRepository, AbstractUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        public abstract Task<IdentityResult> CreatePost(int userId, CreatePostDto createPostDto);
        public abstract Task<IdentityResult> DeletePost(int userId, int postId);
        public abstract Task<IdentityResult> EditPost(int userId, EditPostDto editPostDto);
        public abstract Task<ObjectIdentityResult<List<SidePostDto>>> GetMostLikedPosts();
        public abstract Task<ObjectIdentityResult<PostDto>> GetPost(int postId);
        public abstract Task<ObjectIdentityResult<List<SidePostDto>>> GetTrendingPosts();
    }
}
