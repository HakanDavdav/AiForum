using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.BodyBuilders;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Managers.BotManagers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractBotService : IBotService
    {
        protected readonly AbstractBotRepository _botRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractPostRepository _postRepository;
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractLikeRepository _likeRepository;
        protected readonly AbstractActivityRepository _activityRepository;
        protected readonly AbstractFollowRepository _followRepository;
        protected readonly BotDeployManager _botDeployManager;
        protected readonly NotificationActivityBodyBuilder _notificationActivityBodyBuilder;

        protected AbstractBotService(AbstractBotRepository botRepository, BotDeployManager botDeployManager, AbstractUserRepository userRepository,
            AbstractPostRepository postRepository,AbstractEntryRepository entryRepository,AbstractLikeRepository likeRepository,
            AbstractActivityRepository activityRepository, 
            AbstractFollowRepository followRepository, NotificationActivityBodyBuilder notificationActivityBodyBuilder)
        {
            _botRepository = botRepository;
            _botDeployManager = botDeployManager;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _entryRepository = entryRepository;
            _likeRepository = likeRepository;
            _activityRepository = activityRepository;
            _followRepository = followRepository;
            _activityRepository = activityRepository;
            _notificationActivityBodyBuilder = notificationActivityBodyBuilder;
        }

        public abstract Task<IdentityResult> CreateBot(int userId, CreateBotDto createBotDto);
        public abstract Task<IdentityResult> DeleteBot(int userId, int botId);
        public abstract Task<IdentityResult> DeployBot(int userId, int botId);
        public abstract Task<IdentityResult> EditBot(int userId, EditBotDto editBotDto);
        public abstract Task<ObjectIdentityResult<BotProfileDto>> GetBotProfile(int botId, ClaimsPrincipal claims);
        public abstract Task<ObjectIdentityResult<List<BotActivityDto>>> LoadBotActivities(int botId, int page);
        public abstract Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadBotLikes(int botId, int page);
        public abstract Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowed(int userId, int page);
        public abstract Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowers(int userId, int page);
        public abstract Task<ObjectIdentityResult<List<EntryProfileDto>>> LoadProfileEntries(int botId, ClaimsPrincipal claims, int page);
        public abstract Task<ObjectIdentityResult<List<PostProfileDto>>> LoadProfilePosts(int botId, ClaimsPrincipal claims, int page);
    }
}
