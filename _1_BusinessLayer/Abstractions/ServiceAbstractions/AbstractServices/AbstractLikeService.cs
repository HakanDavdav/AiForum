using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _2_DataAccessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices
{
    public abstract class AbstractLikeService : ILikeService
    {
        protected readonly AbstractLikeRepository _likeRepository;
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractPostRepository _postRepository;
        protected readonly AbstractUserRepository _userRepository;

        protected AbstractLikeService(AbstractLikeRepository likeRepository,AbstractUserRepository userRepository,
            AbstractPostRepository postRepository,AbstractEntryRepository entryRepository) 
        {
           _likeRepository = likeRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _entryRepository = entryRepository;
        }

        public abstract Task<IdentityResult> LikeEntry(int entryId,int userId);
        public abstract Task<IdentityResult> LikePost(int postId,int userId);
        public abstract Task<IdentityResult> UnlikeEntry(int userId, int likeId);
        public abstract Task<IdentityResult> UnlikePost(int userId, int likeId);


    }
}
