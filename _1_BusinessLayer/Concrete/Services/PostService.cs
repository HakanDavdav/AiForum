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
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.MessageBackgroundService;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Extensions.Mappers;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using _2_DataAccessLayer.Concrete.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;

namespace _1_BusinessLayer.Concrete.Services
{
    public class PostService : AbstractPostService
    {
        public PostService(
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
            AbstractGenericCommandHandler genericCommandHandler)
            : base(postQueryHandler, userQueryHandler, entryQueryHandler, likeQueryHandler, followQueryHandler, mailEventFactory, notificationEventFactory, queueSender, notificationQueryHandler, unitOfWork, botQueryHandler, genericCommandHandler)
        {
        }

        public override async Task<IdentityResult> CreatePost(int userId, CreatePostDto createPostDto)
        {
            var user = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Id == userId));
            if (user == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));

            var post = createPostDto.CreatePostDto_To_Post();
            user.Posts.Add(post);
            user.PostCount += 1;

            var follows = await _followQueryHandler.GetWithCustomSearchAsync(q => q.Where(f => f.UserFollowedId == userId).AsNoTracking());
            var toUserIds = follows.Select(f => f.UserFollowerId).ToList();
            var notifications = toUserIds.Select(toUserId => new Notification
            {
                FromUserId = userId,
                OwnerUserId = toUserId,
                NotificationType = NotificationType.CreatingPost,
                AdditionalId = post.PostId,
                AdditionalInfo = post.Title,
                IsRead = false,
                DateTime = DateTime.UtcNow,
            }).ToList();

            await _genericCommandHandler.ManuallyInsertRangeAsync<Notification>(notifications);
            await _genericCommandHandler.SaveChangesAsync();

            var mailEvents = _mailEventFactory.CreateMailEvents(user, null, toUserIds, MailType.CreatingPost, post.Title, post.PostId);
            var notificationEvents = _notificationEventFactory.CreateNotificationEvents(user, null, toUserIds, NotificationType.CreatingPost, post.Title, post.PostId);

            await _queueSender.MailQueueSendAsync(mailEvents);
            await _queueSender.NotificationQueueSendAsync(notificationEvents);

            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeletePost(int userId, int postId)
        {
            var post = await _postQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(p => p.PostId == postId && p.OwnerUserId == userId).Include(p => p.OwnerUser));
            if (post == null)
                return IdentityResult.Failed(new NotFoundError("Post not found or owner is not you"));
            if (post.OwnerUser == null)
                return IdentityResult.Failed(new NotFoundError("Owner user not found"));
            post.OwnerUser.PostCount -= 1;
            await _genericCommandHandler.DeleteAsync<Post>(post);
            await _genericCommandHandler.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> EditPost(int userId, EditPostDto editPostDto)
        {
            var post = await _postQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(p => p.PostId == editPostDto.PostId && p.OwnerUserId == userId).Include(p => p.OwnerUser));
            if (post == null)
                return IdentityResult.Failed(new NotFoundError("Post not found or owner is not you"));
            if (post.OwnerUser == null)
                return IdentityResult.Failed(new NotFoundError("Owner user not found"));
            editPostDto.Update___EditPostDto_To_Post(post);
            await _genericCommandHandler.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public override async Task<ObjectIdentityResult<PostDto>> GetPostAsync(int postId, ClaimsPrincipal claims)
        {
            var startInterval = 0;
            var endInterval = claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10;
            var post = await _postQueryHandler.GetPostModuleAsync(postId);
            if (post == null)
                return ObjectIdentityResult<PostDto>.Failed(null, new IdentityError[] { new NotFoundError("Post not found") });
            post.Entries = await _entryQueryHandler.GetEntryModulesForPostAsync(postId, startInterval, endInterval);
            var postDto = post.Post_To_PostDto();
            return ObjectIdentityResult<PostDto>.Succeded(postDto);
        }

        public override Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(int postCount)
        {
            throw new NotImplementedException();
        }

        public override async Task<ObjectIdentityResult<List<EntryPostDto>>> LoadPostEntries(int postId, ClaimsPrincipal claims, int page)
        {
            var startInterval = (page - 1) * (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var endInterval = startInterval + (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var entries = await _entryQueryHandler.GetEntryModulesForPostAsync(postId, startInterval, endInterval);
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
            var likes = await _likeQueryHandler.GetLikeModulesForPostAsync(postId, startInterval, endInterval);
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            foreach (var like in likes)
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return ObjectIdentityResult<List<MinimalLikeDto>>.Succeded(minimalLikeDtos);
        }
    }
}
