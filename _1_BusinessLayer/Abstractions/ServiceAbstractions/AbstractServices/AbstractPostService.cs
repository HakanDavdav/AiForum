using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.MessageBackgroundService;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Extensions;
using Microsoft.AspNetCore.Identity;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractPostService : IPostService
    {
        protected readonly AbstractPostQueryHandler _postQueryHandler;
        protected readonly AbstractEntryQueryHandler _entryQueryHandler;
        protected readonly AbstractLikeQueryHandler _likeQueryHandler;
        protected readonly AbstractUserQueryHandler _userQueryHandler;
        protected readonly AbstractBotQueryHandler _botQueryHandler;
        protected readonly AbstractFollowQueryHandler _followQueryHandler;
        protected readonly AbstractNotificationQueryHandler _notificationQueryHandler;
        protected readonly AbstractGenericCommandHandler _genericCommandHandler;
        protected readonly MailEventFactory _mailEventFactory;
        protected readonly NotificationEventFactory _notificationEventFactory;
        protected readonly QueueSender _queueSender;
        protected readonly UnitOfWork _unitOfWork;

        protected AbstractPostService(
            AbstractPostQueryHandler postQueryHandler,
            AbstractUserQueryHandler userQueryHandler,
            AbstractEntryQueryHandler entryQueryHandler,
            AbstractLikeQueryHandler likeQueryHandler,
            AbstractFollowQueryHandler followQueryHandler,
            MailEventFactory mailEventFactory,
            NotificationEventFactory notificationEventFactory,
            QueueSender queueSender,
            AbstractNotificationQueryHandler notificationQueryHandler,
            UnitOfWork unitOfWork,
            AbstractBotQueryHandler botQueryHandler,
            AbstractGenericCommandHandler genericCommandHandler
        )
        {
            _unitOfWork = unitOfWork;
            _postQueryHandler = postQueryHandler;
            _userQueryHandler = userQueryHandler;
            _entryQueryHandler = entryQueryHandler;
            _likeQueryHandler = likeQueryHandler;
            _followQueryHandler = followQueryHandler;
            _notificationQueryHandler = notificationQueryHandler;
            _queueSender = queueSender;
            _notificationEventFactory = notificationEventFactory;
            _mailEventFactory = mailEventFactory;
            _botQueryHandler = botQueryHandler;
            _genericCommandHandler = genericCommandHandler;
        }
        public abstract Task<IdentityResult> CreatePost(int userId, CreatePostDto createPostDto);
        public abstract Task<IdentityResult> DeletePost(int userId, int postId);
        public abstract Task<IdentityResult> EditPost(int userId, EditPostDto editPostDto);
        public abstract Task<ObjectIdentityResult<PostDto>> GetPostAsync(int postId, ClaimsPrincipal claims);
        public abstract Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(int postCount);
        public abstract Task<ObjectIdentityResult<List<EntryPostDto>>> LoadPostEntries(int postId, ClaimsPrincipal claims, int page);
        public abstract Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadPostLikes(int postId, int page);
    }
}
