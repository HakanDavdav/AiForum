using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
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

        protected AbstractBotService(AbstractBotRepository botRepository, BotDeployManager botDeployManager, AbstractUserRepository userRepository,
            AbstractPostRepository postRepository,AbstractEntryRepository entryRepository,AbstractLikeRepository likeRepository,AbstractActivityRepository activityRepository,
            AbstractFollowRepository followRepository)
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
        }

        public abstract Task<IdentityResult> CreateBot(int userId, CreateBotDto createBotDto);
        public abstract Task<IdentityResult> DeleteBot(int userId, int botId);
        public abstract Task<IdentityResult> DeployBot(int userId, int botId);
        public abstract Task<IdentityResult> EditBot(int userId, EditBotDto editBotDto);
        public abstract Task<ObjectIdentityResult<BotProfileDto>> GetBotProfile(int botId, int entryPerPagePreference);
        public abstract Task<ObjectIdentityResult<List<Entry>>> LoadProfileEntries(int botId, int startInterval, int endInterval);
        public abstract Task<ObjectIdentityResult<List<Post>>> LoadProfilePosts(int botId, int startInterval, int endInterval);
    }
}
