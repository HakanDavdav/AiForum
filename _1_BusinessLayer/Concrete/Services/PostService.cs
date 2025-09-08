using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _1_BusinessLayer.Concrete.Tools.MessageBackgroundService;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Services
{
    public class PostService : AbstractPostService
    {
        public PostService(AbstractPostRepository postRepository, AbstractUserRepository userRepository, 
            AbstractEntryRepository entryRepository, AbstractLikeRepository likeRepository, 
            AbstractFollowRepository followRepository, MailEventFactory mailEventFactory, 
            NotificationEventFactory notificationEventFactory, QueueSender queueSender, 
            AbstractNotificationRepository notificationRepository) 
            : base(postRepository, userRepository, entryRepository, likeRepository, followRepository, mailEventFactory, notificationEventFactory, queueSender, notificationRepository)
        {
        }

        public override async Task<IdentityResult> CreatePost(int userId, CreatePostDto createPostDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var post = createPostDto.CreatePostDto_To_Post(userId);
                user.Posts.Add(post);
                user.PostCount += 1;
                var follows = await _followRepository.GetWithCustomSearchAsync(query => query.Where(follow => follow.UserFollowedId == userId).AsNoTracking());
                var userIds = new List<int?>();
                for (global::System.Int32 i = 0; i < follows.Count; i++)
                {
                    userIds.Add(follows[i].UserFollowedId);
                }                
                await _postRepository.SaveChangesAsync();

                var mailEvents = _mailEventFactory.CreateMailEvents(user, null, userIds, MailType.CreatingPost, post.Title, post.PostId);
                var notificationEvents = _notificationEventFactory.CreateNotificationEvents(user, null, userIds, NotificationType.CreatingPost, post.Title, post.PostId);
                var notifications = new List<Notification>();
                foreach (var toUserId in userIds)
                {
                    notifications.Add(new Notification
                    {
                        FromUserId = userId,
                        OwnerUserId = toUserId,
                        NotificationType = NotificationType.CreatingPost,
                        AdditionalId = post.PostId,
                        AdditionalInfo = post.Title,
                        IsRead = false,
                        DateTime = DateTime.UtcNow,
                    });
                }
                await _notificationRepository.ManuallyInsertRangeAsync(notifications);               
                await _queueSender.MailQueueSendAsync(mailEvents);
                await _queueSender.NotificationQueueSendAsync(notificationEvents);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("Sender User not found"));
        }

        public override async Task<IdentityResult> DeletePost(int userId, int postId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var post = await _postRepository.GetByIdAsync(postId);
                if (post != null)
                {
                    if (post.OwnerUserId == userId)
                    {
                        await _postRepository.DeleteAsync(post);
                        user.PostCount -= 1;
                        await _postRepository.SaveChangesAsync();
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(new UnauthorizedError("ParentUser does not have that kind of post:)"));
                }
                return IdentityResult.Failed(new NotFoundError("Post not found"));
            }
            return IdentityResult.Failed(new NotFoundError("Post not found"));
        }

        public override async Task<IdentityResult> EditPost(int userId, EditPostDto editPostDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var post = await _postRepository.GetByIdAsync(editPostDto.PostId);
                if (post != null)
                {
                    if (post.OwnerUserId == userId)
                    {
                        post = editPostDto.Update___EditPostDto_To_Post(post);
                        await _postRepository.SaveChangesAsync();
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(new UnauthorizedError("ParentUser does not have that kind of post:)"));
                }
                return IdentityResult.Failed(new NotFoundError("Post not found"));
            }
            return IdentityResult.Failed(new NotFoundError("ParentUser not found"));
        }

        public override async Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(DateTime date, ClaimsPrincipal claims)
        {
            var postCount = claims.FindFirst("PostPerPage") != null ? int.Parse(claims.FindFirst("PostPerPage").Value) : 30;
            List<MinimalPostDto> minimalPostDtos = new List<MinimalPostDto>();
            List<Post> posts = await _postRepository.GetWithCustomSearchAsync(q =>
                q.Where(p => p.DateTime.Date == date.Date)
                .OrderByDescending(p => p.Likes)
                .Take(postCount)
                 );
            foreach (var post in posts)
            {
                minimalPostDtos.Add(post.Post_To_MinimalPostDto());
            }
            return ObjectIdentityResult<List<MinimalPostDto>>.Succeded(minimalPostDtos);
        }

        public override async Task<ObjectIdentityResult<PostDto>> GetPostAsync(int postId, ClaimsPrincipal claims)
        {
            var startInterval = 0;
            var endInterval = claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10;
            var post = await _postRepository.GetPostModuleAsync(postId);
            if (post != null)
            {
                post.Entries = await _entryRepository.GetEntryModulesForPostAsync(postId, startInterval, endInterval);
                var postDto = post.Post_To_PostDto();
                return ObjectIdentityResult<PostDto>.Succeded(postDto);
            }
            return ObjectIdentityResult<PostDto>.Failed(null, new IdentityError[] { new NotFoundError("Post not found") });

        }


        public override Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(int postCount)
        {
            throw new NotImplementedException();
        }

        public override async Task<ObjectIdentityResult<List<EntryPostDto>>> LoadPostEntries(int postId, ClaimsPrincipal claims, int page)
        {
            var startInterval = (page - 1) * (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var endInterval = startInterval + (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var entries = await _entryRepository.GetEntryModulesForPostAsync(postId, startInterval, endInterval);
            List<EntryPostDto> entryPostDtos = new List<EntryPostDto>();
            foreach (var entry in entries)
            {
                entryPostDtos.Add(entry.Entry_To_EntryPostDto());
            }
            return ObjectIdentityResult<List<EntryPostDto>>.Succeded(entryPostDtos);
        }

        public override async Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadPostLikes(int postId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var likes = await _likeRepository.GetLikeModulesForPost(postId, startInterval, endInterval);
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            foreach (var like in likes)
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return ObjectIdentityResult<List<MinimalLikeDto>>.Succeded(minimalLikeDtos);
        }
    }
}
