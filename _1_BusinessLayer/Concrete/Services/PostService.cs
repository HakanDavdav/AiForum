using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class PostService : AbstractPostService
    {
        public PostService(AbstractLikeRepository likeRepository, AbstractPostRepository postRepository) : base(likeRepository, postRepository)
        {
        }

        public override Task<IdentityResult> CreateComplaint(int userId, int postId)
        {
            throw new NotImplementedException();
        }

        public override async Task<IdentityResult> CreatePost(int userId, string title, string context)
        {
            await _postRepository.InsertAsync(new Post
            {
                Title = title,
                Context = context,
                UserId = userId
            });
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeletePost(int userId, int postId)
        {
            var post = await _postRepository.GetByIdWithInfoAsync(postId);
            if (post.UserId == userId)
            {
                await _postRepository.DeleteAsync(post);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("Unauthorized deletion"));
        }


        public override async Task<IdentityResult> LikePost(int userId, int postId)
        {
            await _likeRepository.InsertAsync(new Like
            {
                UserId = userId,
                PostId = postId
            });
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> UnlikePost(int userId, int likeId)
        {
            var like = await _likeRepository.GetByIdWithInfoAsync(likeId);
            if (like.UserId == userId)
            {
                await _likeRepository.DeleteAsync(like);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("Unauthorized deletion"));
        }

        public override async Task<IdentityResult> UpdatePost(int userId, int postId, string title, string context)
        {
            var post = await _postRepository.GetByIdWithInfoAsync(postId);
            if (post.UserId == userId)
            {
                post.Context = context;
                post.Title = title;
                await _postRepository.UpdateAsync(post);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("Unauthorized deletion"));
        }
    }
}
