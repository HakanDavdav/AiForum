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
    public class UserInteractionService : AbstractInteractionService
    {
        public UserInteractionService(AbstractUserRepository userRepository, AbstractEntryRepository entryRepository, 
            AbstractFollowRepository followRepository, AbstractLikeRepository likeRepository, AbstractPostRepository postRepository) 
            : base(userRepository, entryRepository, followRepository, likeRepository, postRepository)
        {
        }

        public override Task<IdentityResult> CreateComplaint(int userId, int? entryId, int? postId)
        {
            throw new NotImplementedException();
        }

        public override async Task<IdentityResult> CreateEntry(int userId, int postId, string context)
        {
            await _entryRepository.InsertAsync(new Entry
            {
                UserId = userId,
                PostId = postId,
                Context = context
            });
            return IdentityResult.Success;
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

        public override async Task<IdentityResult> DeleteEntry(int userId, int entryId)
        {
            var entry = await _entryRepository.GetByIdWithInfoAsync(entryId);
            if(entry.UserId == userId)
            {
                await _entryRepository.DeleteAsync(entry);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("Unauthorized deletion"));
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

        public override async Task<IdentityResult> Follow(int userId, int followedUserId)
        {
            await _followRepository.InsertAsync(new Follow
            {
                FolloweeId = userId,
                FollowedId = followedUserId
            });
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> LikeEntry(int userId, int entryId)
        {
            await _likeRepository.InsertAsync(new Like
            {
                UserId = userId,
                EntryId = entryId
            });
            return IdentityResult.Success;
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

        public override async Task<IdentityResult> Unfollow(int userId, int followedUserId, int followId)
        {
            var follow = await _followRepository.GetByIdWithInfoAsync(followId);
            if (follow.FolloweeId == userId&&follow.FollowedId==followedUserId) 
            {
                await _followRepository.DeleteAsync(follow);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("Unauthorized deletion"));
        }

        public override async Task<IdentityResult> UnlikeEntry(int userId, int likeId)
        {
            var like = await _likeRepository.GetByIdWithInfoAsync(likeId);
            if(like.UserId == userId)
            {
                await _likeRepository.DeleteAsync(like);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("Unauthorized deletion"));
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

        public override async Task<IdentityResult> UpdateEntry(int userId, int entryId, string context)
        {
            var entry = await _entryRepository.GetByIdWithInfoAsync(entryId);
            if (entry.UserId == userId)
            {
                entry.Context = context;
                await _entryRepository.UpdateAsync(entry);
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
