using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.MessageBackgroundService;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Extensions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractEntryService : IEntryService
    {
        protected readonly AbstractEntryQueryHandler _entryQueryHandler;
        protected readonly AbstractUserQueryHandler _userQueryHandler;
        protected readonly AbstractLikeQueryHandler _likeQueryHandler;
        protected readonly AbstractPostQueryHandler _postQueryHandler;
        protected readonly AbstractFollowQueryHandler _followQueryHandler;
        protected readonly AbstractNotificationQueryHandler _notificationQueryHandler;
        protected readonly AbstractGenericCommandHandler _genericCommandHandler;
        protected readonly MailEventFactory _mailEventFactory;
        protected readonly NotificationEventFactory _notificationEventFactory;
        protected readonly QueueSender _queueSender;
        protected readonly UnitOfWork _unitOfWork;


        protected AbstractEntryService(AbstractLikeQueryHandler likeQueryHandler,AbstractEntryQueryHandler entryQueryHandler,AbstractPostQueryHandler postQueryHandler,
            AbstractFollowQueryHandler followQueryHandler,AbstractUserQueryHandler userQueryHandler,AbstractNotificationQueryHandler abstractNotificationQueryHandler,
            MailEventFactory mailEventFactory, QueueSender queueSender, UnitOfWork unitOfWork, NotificationEventFactory notificationEventFactory,AbstractGenericCommandHandler genericCommandHandler)
        {
            _entryQueryHandler = entryQueryHandler;
            _userQueryHandler = userQueryHandler;
            _likeQueryHandler = likeQueryHandler;
            _postQueryHandler = postQueryHandler;
            _followQueryHandler = followQueryHandler;
            _notificationQueryHandler = abstractNotificationQueryHandler;
            _mailEventFactory = mailEventFactory;
            _queueSender = queueSender;
            _unitOfWork = unitOfWork;
            _notificationEventFactory = notificationEventFactory;
        }

        public abstract Task<IdentityResult> CreateEntryAsync(int userId, int postId, CreateEntryDto createEntryDto);
        public abstract Task<IdentityResult> DeleteEntryAsync(int userId, int entryId);
        public abstract Task<IdentityResult> EditEntryAsync(int userId, EditEntryDto editEntryDto);
        public abstract Task<ObjectIdentityResult<EntryProfileDto>> GetEntryAsync(int entryId);
        public abstract Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadEntryLikes(int entryId, int page);
    }
}
