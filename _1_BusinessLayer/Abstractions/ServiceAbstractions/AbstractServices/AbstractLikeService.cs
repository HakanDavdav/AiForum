using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.MessageBackgroundService;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Extensions;
using Microsoft.AspNetCore.Identity;
using _2_DataAccessLayer.Abstractions.AbstractClasses; // For query handlers
using _2_DataAccessLayer.Abstractions.Generic; // For AbstractGenericCommandHandler

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractLikeService : ILikeService
    {
        protected readonly AbstractNotificationQueryHandler _notificationQueryHandler;
        protected readonly AbstractEntryQueryHandler _entryQueryHandler;
        protected readonly AbstractPostQueryHandler _postQueryHandler;
        protected readonly AbstractUserQueryHandler _userQueryHandler;
        protected readonly AbstractBotQueryHandler _botQueryHandler;
        protected readonly AbstractFollowQueryHandler _followQueryHandler;
        protected readonly AbstractLikeQueryHandler _likeQueryHandler;
        protected readonly AbstractGenericCommandHandler _genericCommandHandler;
        protected readonly MailEventFactory _mailEventFactory;
        protected readonly NotificationEventFactory _notificationEventFactory;
        protected readonly QueueSender _queueSender;
        protected readonly UnitOfWork _unitOfWork;

        protected AbstractLikeService(
            AbstractNotificationQueryHandler notificationQueryHandler,
            AbstractEntryQueryHandler entryQueryHandler,
            AbstractPostQueryHandler postQueryHandler,
            AbstractUserQueryHandler userQueryHandler,
            AbstractBotQueryHandler botQueryHandler,
            AbstractFollowQueryHandler followQueryHandler,
            AbstractLikeQueryHandler likeQueryHandler,
            AbstractGenericCommandHandler genericCommandHandler,
            MailEventFactory mailEventFactory,
            NotificationEventFactory notificationEventFactory,
            QueueSender queueSender,
            UnitOfWork unitOfWork
        )
        {
            _notificationQueryHandler = notificationQueryHandler;
            _entryQueryHandler = entryQueryHandler;
            _postQueryHandler = postQueryHandler;
            _userQueryHandler = userQueryHandler;
            _botQueryHandler = botQueryHandler;
            _followQueryHandler = followQueryHandler;
            _likeQueryHandler = likeQueryHandler;
            _genericCommandHandler = genericCommandHandler;
            _mailEventFactory = mailEventFactory;
            _notificationEventFactory = notificationEventFactory;
            _queueSender = queueSender;
            _unitOfWork = unitOfWork;
        }

        public abstract Task<IdentityResult> LikeEntry(int entryId,int userId);
        public abstract Task<IdentityResult> LikePost(int postId,int userId);
        public abstract Task<IdentityResult> UnlikeEntry(int userId, int likeId);
        public abstract Task<IdentityResult> UnlikePost(int userId, int likeId);
    }
}
