using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class PostService : AbstractPostService
    {
        public PostService(AbstractPostRepository postRepository, AbstractUserRepository userRepository) : base(postRepository, userRepository)
        {
        }

        public override async Task<IdentityResult> CreatePost(int userId, CreatePostDto createPostDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var post = createPostDto.CreatePostDto_To_Post(user);
                await _postRepository.InsertAsync(post);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> DeletePost(int userId, int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post != null)
            {
                if (post.UserId == userId)
                {
                    await _postRepository.DeleteAsync(post);
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnauthorizedError("User does not have that kind of post:)"));
            }
            return IdentityResult.Failed(new NotFoundError("Post not found"));
        }

        public override async Task<IdentityResult> EditPost(int userId, EditPostDto editPostDto)
        {
            var post = await _postRepository.GetByIdAsync(editPostDto.PostId);
            if (post != null)
            {
                if (post.UserId == userId)
                {
                    post = editPostDto.Update___EditPostDto_To_Post(post);
                    await _postRepository.UpdateAsync(post);
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnauthorizedError("User does not have that kind of post:)"));
            }
            return IdentityResult.Failed(new NotFoundError("Post not found"));
        }

        public override Task<ObjectIdentityResult<List<SidePostDto>>> GetMostLikedPosts()
        {
            throw new NotImplementedException();
        }

        public override Task<ObjectIdentityResult<PostDto>> GetPost(int postId)
        {
            throw new NotImplementedException();
        }

        public override Task<ObjectIdentityResult<List<SidePostDto>>> GetTrendingPosts()
        {
            throw new NotImplementedException();
        }
    }
}
