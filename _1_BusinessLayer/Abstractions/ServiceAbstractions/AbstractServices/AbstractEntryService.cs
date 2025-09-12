using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _1_BusinessLayer.Concrete.Tools.MessageBackgroundService;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Extensions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractEntryService : IEntryService
    {
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractLikeRepository _likeRepository;
        protected readonly AbstractPostRepository _postRepository;
        protected readonly AbstractFollowRepository _followRepository;
        protected readonly AbstractNotificationRepository _notificationRepository;
        protected readonly MailEventFactory _mailEventFactory;
        protected readonly NotificationEventFactory _notificationEventFactory;
        protected readonly QueueSender _queueSender;
        protected readonly UnitOfWork _unitOfWork;


        protected AbstractEntryService(AbstractEntryRepository entryRepository, AbstractUserRepository userRepository,
            AbstractLikeRepository likeRepository, AbstractPostRepository postRepository,AbstractFollowRepository followRepository,
            MailEventFactory mailEventFactory, NotificationEventFactory notificationEventFactory, QueueSender queueSender, 
            AbstractNotificationRepository notificationRepository,UnitOfWork unitOfWork)
        {
            _entryRepository = entryRepository;
            _userRepository = userRepository;
            _likeRepository = likeRepository;
            _postRepository = postRepository;
            _followRepository = followRepository;
            _notificationRepository = notificationRepository;
            _notificationEventFactory = notificationEventFactory;
            _mailEventFactory = mailEventFactory;
            _queueSender = queueSender;
            _unitOfWork = unitOfWork;

        }

        public abstract Task<IdentityResult> CreateEntryAsync(int userId, int postId, CreateEntryDto createEntryDto);
        public abstract Task<IdentityResult> DeleteEntryAsync(int userId, int entryId);
        public abstract Task<IdentityResult> EditEntryAsync(int userId, EditEntryDto editEntryDto);
        public abstract Task<ObjectIdentityResult<EntryProfileDto>> GetEntryAsync(int entryId);
        public abstract Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadEntryLikes(int entryId, int page);
    }
}
