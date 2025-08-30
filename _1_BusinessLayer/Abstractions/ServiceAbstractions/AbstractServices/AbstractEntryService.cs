using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Managers.UserToolManagers;
using _2_DataAccessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices
{
    public abstract class AbstractEntryService : IEntryService
    {
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractLikeRepository _likeRepository;
        protected readonly AbstractPostRepository _postRepository;
        protected readonly ActivityBaseManager _activityBaseManager;
        protected readonly AbstractFollowRepository _followRepository;

        protected AbstractEntryService(AbstractEntryRepository entryRepository, AbstractUserRepository userRepository,
            AbstractLikeRepository likeRepository, AbstractPostRepository postRepository,AbstractFollowRepository followRepository, ActivityBaseManager activityBaseManager)
        {
            _entryRepository = entryRepository;
            _userRepository = userRepository;
            _likeRepository = likeRepository;
            _postRepository = postRepository;
            _followRepository = followRepository;
            _activityBaseManager = activityBaseManager;

        }

        public abstract Task<IdentityResult> CreateEntryAsync(int userId, int postId, CreateEntryDto createEntryDto);
        public abstract Task<IdentityResult> DeleteEntryAsync(int userId, int entryId);
        public abstract Task<IdentityResult> EditEntryAsync(int userId, EditEntryDto editEntryDto);
        public abstract Task<ObjectIdentityResult<EntryProfileDto>> GetEntryAsync(int entryId);
        public abstract Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadEntryLikes(int entryId, int startInterval, int endInterval);
    }
}
