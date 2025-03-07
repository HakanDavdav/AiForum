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
    public abstract class AbstractInteractionService : IInteractionService
    {
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractFollowRepository _followRepository;
        protected readonly AbstractLikeRepository _likeRepository;
        protected readonly AbstractPostRepository _postRepository;
        protected AbstractInteractionService(AbstractUserRepository userRepository,AbstractEntryRepository entryRepository,
            AbstractFollowRepository followRepository, AbstractLikeRepository likeRepository, AbstractPostRepository postRepository)
        {
            _entryRepository = entryRepository;
            _followRepository = followRepository;
            _likeRepository = likeRepository;
            _followRepository = followRepository;
            _postRepository = postRepository;
        }

        public abstract Task<IdentityResult> CreateComplaint(int userId, int? entryId, int? postId);
        public abstract Task<IdentityResult> CreateEntry(int userId, int postId, string context);
        public abstract Task<IdentityResult> CreatePost(int userId, string title, string context);
        public abstract Task<IdentityResult> DeleteEntry(int userId, int entryId);
        public abstract Task<IdentityResult> DeletePost(int userId, int postId);
        public abstract Task<IdentityResult> Follow(int userId, int followedUserId);
        public abstract Task<IdentityResult> LikeEntry(int userId, int entryId);
        public abstract Task<IdentityResult> LikePost(int userId, int postId);
        public abstract Task<IdentityResult> Unfollow(int userId, int followedUserId, int followId);
        public abstract Task<IdentityResult> UnlikeEntry(int userId, int entryId);
        public abstract Task<IdentityResult> UnlikePost(int userId, int postId);
        public abstract Task<IdentityResult> UpdateEntry(int userId, int postId, string context);
        public abstract Task<IdentityResult> UpdatePost(int userId, int postId, string title, string context);
    }
}
