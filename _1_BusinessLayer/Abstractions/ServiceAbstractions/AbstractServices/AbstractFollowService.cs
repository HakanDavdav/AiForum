using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.MessageBackgroundService;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Concrete.Extensions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractFollowService : IFollowService
    {
        protected readonly AbstractFollowQueryHandler _followQueryHandler;
        protected readonly AbstractUserQueryHandler _userQueryHandler;
        protected readonly AbstractBotQueryHandler _botQueryHandler;
        protected readonly MailEventFactory _mailEventFactory;
        protected readonly NotificationEventFactory _notificationEventFactory;
        protected readonly QueueSender _queueSender;
        protected readonly UnitOfWork _unitOfWork;

        protected AbstractFollowService(
            AbstractFollowQueryHandler followQueryHandler,
            AbstractUserQueryHandler userQueryHandler,
            AbstractBotQueryHandler botQueryHandler,
            MailEventFactory mailEventFactory,
            NotificationEventFactory notificationEventFactory,
            QueueSender queueSender,
            UnitOfWork unitOfWork)
        {
            _followQueryHandler = followQueryHandler;
            _userQueryHandler = userQueryHandler;
            _botQueryHandler = botQueryHandler;
            _mailEventFactory = mailEventFactory;
            _notificationEventFactory = notificationEventFactory;
            _queueSender = queueSender;
            _unitOfWork = unitOfWork;
        }

        public abstract Task<IdentityResult> DeleteFollow(int userId, int followId);
        public abstract Task<IdentityResult> FollowBot(int userId, int followedBotId);
        public abstract Task<IdentityResult> FollowUser(int userId, int followedUserId);
    }
}
