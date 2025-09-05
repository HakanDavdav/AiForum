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
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _1_BusinessLayer.Concrete.Tools.MessageBackgroundService;
using _2_DataAccessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractPostService : IPostService
    {
        protected readonly AbstractPostRepository _postRepository;
        protected readonly AbstractEntryRepository _entryRepository;
        protected readonly AbstractLikeRepository _likeRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractBotRepository _botRepository;
        protected readonly AbstractFollowRepository _followRepository;
        protected readonly AbstractNotificationRepository _notificationRepository;
        protected readonly MailEventFactory _mailEventFactory;
        protected readonly NotificationEventFactory _notificationEventFactory;
        protected readonly QueueSender _queueSender;

        protected AbstractPostService(AbstractPostRepository postRepository, AbstractUserRepository userRepository, 
            AbstractEntryRepository entryRepository, AbstractLikeRepository likeRepository, AbstractFollowRepository followRepository,
            MailEventFactory mailEventFactory, NotificationEventFactory notificationEventFactory,QueueSender queueSender,AbstractNotificationRepository notificationRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _entryRepository = entryRepository;
            _likeRepository = likeRepository;
            _followRepository = followRepository;
            _notificationRepository = notificationRepository;
            _queueSender = queueSender;
            _notificationEventFactory = notificationEventFactory;
            _mailEventFactory = mailEventFactory;
        }
        public abstract Task<IdentityResult> CreatePost(int userId, CreatePostDto createPostDto);
        public abstract Task<IdentityResult> DeletePost(int userId, int postId);
        public abstract Task<IdentityResult> EditPost(int userId, EditPostDto editPostDto);
        public abstract Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(DateTime date, ClaimsPrincipal claims);
        public abstract Task<ObjectIdentityResult<PostDto>> GetPostAsync(int postId, ClaimsPrincipal claims);
        public abstract Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(int postCount);
        public abstract Task<ObjectIdentityResult<List<EntryPostDto>>> LoadPostEntries(int postId, ClaimsPrincipal claims, int page);
        public abstract Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadPostLikes(int postId, int page);
    }
}
