using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _1_BusinessLayer.Concrete.Tools.MessageBackgroundService;
using _2_DataAccessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractFollowService : IFollowService
    {
        protected readonly AbstractFollowRepository _followRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractBotRepository _botRepository;
        protected readonly MailEventFactory _mailEventFactory;
        protected readonly NotificationEventFactory _notificationEventFactory;
        protected readonly QueueSender _queueSender;

        public AbstractFollowService(AbstractFollowRepository followRepository, AbstractUserRepository userRepository, AbstractBotRepository botRepository,
            MailEventFactory mailEventFactory, NotificationEventFactory notificationEventFactory, QueueSender queueSender)
        {
            _followRepository = followRepository;
            _userRepository = userRepository;
            _botRepository = botRepository;
            _mailEventFactory = mailEventFactory;
            _notificationEventFactory = notificationEventFactory;
            _queueSender = queueSender;

        }

        public abstract Task<IdentityResult> DeleteFollow(int userId, int followId);
        public abstract Task<IdentityResult> FollowBot(int userId, int followedBotId);
        public abstract Task<IdentityResult> FollowUser(int userId, int followedUserId);
    }
}
