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
        protected readonly AbstractUserRepository _userRepository;

        protected AbstractLikeService(AbstractLikeRepository likeRepository,AbstractUserRepository userRepository) 
        {
           _likeRepository = likeRepository;
            _userRepository = userRepository;
        }

        public abstract Task<IdentityResult> LikeEntry(int entryId);
        public abstract Task<IdentityResult> LikePost(int postId);
        public abstract Task<IdentityResult> UnlikeEntry(int entryId);
        public abstract Task<IdentityResult> UnlikePost(int postId);
    }
}
