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
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers;
using _1_BusinessLayer.Concrete.Tools.BodyBuilders;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractBotService : IBotService
    {
        protected readonly AbstractBotQueryHandler _botQueryHandler;
        protected readonly AbstractBotActivityQueryHandler _botActivityQueryHandler;
        protected readonly AbstractEntryQueryHandler _entryQueryHandler;
        protected readonly AbstractPostQueryHandler _postQueryHandler;
        protected readonly AbstractLikeQueryHandler _likeQueryHandler;
        protected readonly AbstractFollowQueryHandler _followQueryHandler;
        protected readonly AbstractUserQueryHandler _userQueryHandler;
        protected readonly AbstractGenericCommandHandler _genericCommandHandler;
        protected readonly BotOrchestrator _botDeployManager;
        protected readonly NotificationActivityBodyBuilder _notificationActivityBodyBuilder;

        protected AbstractBotService(
            AbstractBotQueryHandler botQueryHandler,
            AbstractBotActivityQueryHandler botActivityQueryHandler,
            AbstractEntryQueryHandler entryQueryHandler,
            AbstractPostQueryHandler postQueryHandler,
            AbstractLikeQueryHandler likeQueryHandler,
            AbstractFollowQueryHandler followQueryHandler,
            AbstractUserQueryHandler userQueryHandler,
            AbstractGenericCommandHandler commandHandler,
            BotOrchestrator botDeployManager,
            NotificationActivityBodyBuilder notificationActivityBodyBuilder)
        {
            _botQueryHandler = botQueryHandler;
            _botActivityQueryHandler = botActivityQueryHandler;
            _entryQueryHandler = entryQueryHandler;
            _postQueryHandler = postQueryHandler;
            _likeQueryHandler = likeQueryHandler;
            _followQueryHandler = followQueryHandler;
            _userQueryHandler = userQueryHandler;
            _genericCommandHandler = commandHandler;
            _botDeployManager = botDeployManager;
            _notificationActivityBodyBuilder = notificationActivityBodyBuilder;
        }

        public abstract Task<IdentityResult> CreateBot(int userId, CreateBotDto createBotDto);
        public abstract Task<IdentityResult> DeleteBot(int userId, int botId);
        public abstract Task<IdentityResult> DeployBot(int userId, int botId);
        public abstract Task<ObjectIdentityResult<BotProfileDto>> GetBotProfile(int botId, ClaimsPrincipal claims);
        public abstract Task<ObjectIdentityResult<MinimalBotDto>> GetBotWithChildBotsTree(int botId);
        public abstract Task<ObjectIdentityResult<List<BotActivityDto>>> LoadBotActivities(int botId, int page);
        public abstract Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadBotLikes(int botId, int page);
        public abstract Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowed(int userId, int page);
        public abstract Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowers(int userId, int page);
        public abstract Task<ObjectIdentityResult<List<EntryProfileDto>>> LoadProfileEntries(int botId, ClaimsPrincipal claims, int page);
        public abstract Task<ObjectIdentityResult<List<PostProfileDto>>> LoadProfilePosts(int botId, ClaimsPrincipal claims, int page);
    }
}
