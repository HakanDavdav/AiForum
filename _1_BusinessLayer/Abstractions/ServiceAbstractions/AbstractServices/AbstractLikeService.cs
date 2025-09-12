using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _1_BusinessLayer.Concrete.Tools.MessageBackgroundService;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Extensions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractLikeService : ILikeService
    {
        protected readonly AbstractNotificationRepository _notificationRepository;
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractPostRepository _postRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractBotRepository _botRepository;
        protected readonly AbstractFollowRepository _followRepository;
        protected readonly AbstractLikeRepository _likeRepository;
        protected readonly MailEventFactory _mailEventFactory;
        protected readonly NotificationEventFactory _notificationEventFactory;
        protected readonly QueueSender _queueSender;
        protected readonly UnitOfWork _unitOfWork;


        protected AbstractLikeService(AbstractLikeRepository likeRepository,AbstractUserRepository userRepository,
            AbstractPostRepository postRepository,AbstractEntryRepository entryRepository,MailEventFactory mailEventFactory, AbstractNotificationRepository notificationRepository,
            NotificationEventFactory notificationEventFactory, QueueSender queueSender, AbstractFollowRepository followRepository, AbstractBotRepository botRepository, UnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
            _notificationRepository = notificationRepository;
            _likeRepository = likeRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _entryRepository = entryRepository;
            _followRepository = followRepository;
            _botRepository = botRepository;
            _notificationEventFactory = notificationEventFactory;
            _mailEventFactory = mailEventFactory;
            _queueSender = queueSender;

        }

        public abstract Task<IdentityResult> LikeEntry(int entryId,int userId);
        public abstract Task<IdentityResult> LikePost(int postId,int userId);
        public abstract Task<IdentityResult> UnlikeEntry(int userId, int likeId);
        public abstract Task<IdentityResult> UnlikePost(int userId, int likeId);


    }
}
