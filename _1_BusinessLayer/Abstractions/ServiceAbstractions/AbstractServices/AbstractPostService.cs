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
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractLikeRepository _likeRepository;
        protected readonly AbstractUserRepository _userRepository;

        protected AbstractPostService(AbstractPostRepository postRepository, AbstractUserRepository userRepository, 
            AbstractEntryRepository entryRepository, AbstractLikeRepository likeRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _entryRepository = entryRepository;
            _likeRepository = likeRepository;
        }
        public abstract Task<IdentityResult> CreatePost(int userId, CreatePostDto createPostDto);
        public abstract Task<IdentityResult> DeletePost(int userId, int postId);
        public abstract Task<IdentityResult> EditPost(int userId, EditPostDto editPostDto);
        public abstract Task<ObjectIdentityResult<List<SidePostDto>>> GetMostLikedPosts(string postPerPagePreference);
        public abstract Task<ObjectIdentityResult<PostDto>> GetPost(int postId, string entryPerPagePreference);
        public abstract Task<ObjectIdentityResult<List<SidePostDto>>> GetTrendingPosts(string entryPerPagePreference);
    }
}
