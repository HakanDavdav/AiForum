using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Tools.Managers.UserToolManagers;
using _2_DataAccessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractLikeService : ILikeService
    {
        protected readonly AbstractLikeRepository _likeRepository;
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractPostRepository _postRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly ActivityBaseManager _activityBaseManager;

        protected AbstractLikeService(AbstractLikeRepository likeRepository,AbstractUserRepository userRepository,
            AbstractPostRepository postRepository,AbstractEntryRepository entryRepository, ActivityBaseManager activityBaseManager) 
        {
           _likeRepository = likeRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _entryRepository = entryRepository;
            _activityBaseManager = activityBaseManager;
        }

        public abstract Task<IdentityResult> LikeEntry(int entryId,int userId);
        public abstract Task<IdentityResult> LikePost(int postId,int userId);
        public abstract Task<IdentityResult> UnlikeEntry(int userId, int likeId);
        public abstract Task<IdentityResult> UnlikePost(int userId, int likeId);


    }
}
