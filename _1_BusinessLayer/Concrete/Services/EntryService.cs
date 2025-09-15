using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Events;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.MessageBackgroundService;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Extensions.Mappers;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Services
{
    public class EntryService : AbstractEntryService
    {
        public EntryService(AbstractLikeQueryHandler likeQueryHandler, AbstractEntryQueryHandler entryQueryHandler, AbstractPostQueryHandler postQueryHandler, AbstractFollowQueryHandler followQueryHandler, AbstractUserQueryHandler userQueryHandler, AbstractNotificationQueryHandler abstractNotificationQueryHandler, MailEventFactory mailEventFactory, QueueSender queueSender, UnitOfWork unitOfWork, NotificationEventFactory notificationEventFactory, AbstractGenericCommandHandler genericCommandHandler) : base(likeQueryHandler, entryQueryHandler, postQueryHandler, followQueryHandler, userQueryHandler, abstractNotificationQueryHandler, mailEventFactory, queueSender, unitOfWork, notificationEventFactory, genericCommandHandler)
        {
        }

        public override async Task<IdentityResult> CreateEntryAsync(int userId, int postId, CreateEntryDto createEntryDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                object postOwner = null;
                var post = await _postQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(p => p.PostId == postId).Include(p => p.OwnerUser).Include(p => p.OwnerBot));
                if (post == null) 
                    return IdentityResult.Failed(new NotFoundError("Post not found"));
                if (post.OwnerUser == null && post.OwnerBot == null)
                    return IdentityResult.Failed(new NotFoundError("Post owner not found"));
                if (post.OwnerUser != null)
                    postOwner = post.OwnerUser;
                if (post.OwnerBot != null)
                    postOwner = post.OwnerBot;
                var entryCreatorUser = await _userQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(u => u.Id == userId));
                if (entryCreatorUser == null) 
                    return IdentityResult.Failed(new NotFoundError("User not found"));
                var entry = createEntryDto.CreateEntryDto_To_Entry();
                var follows = await _followQueryHandler.GetWithCustomSearchAsync(query => query.Where(follow => follow.UserFollowedId == userId).AsNoTracking());
                var toUserIds = follows.Select(follow => follow.UserFollowerId).ToList();
                var creatorUserFollowerNotifications = new List<Notification>();
                var mailEvents = new List<MailEvent>();
                var notificationEvents = new List<NotificationEvent>();

                foreach (var toUserId in toUserIds)
                {
                    creatorUserFollowerNotifications.Add(new Notification
                    {
                        FromUserId = userId,
                        OwnerUserId = toUserId,
                        NotificationType = NotificationType.CreatingEntry,
                        AdditionalInfo = entry.Context,
                        AdditionalId = entry.EntryId,
                        IsRead = false,
                        DateTime = DateTime.UtcNow,
                    });
                }
                await _genericCommandHandler.ManuallyInsertRangeAsync<Notification>(creatorUserFollowerNotifications);
                post.Entries.Add(entry);
                entryCreatorUser.Entries.Add(entry);
                post.EntryCount += 1;
                entryCreatorUser.EntryCount += 1;
                await _genericCommandHandler.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                if (postOwner is User postOwnerUser)
                {
                    var postOwnerNotification = new Notification
                    {
                        FromUserId = userId,
                        OwnerUserId = post.OwnerUserId,
                        NotificationType = NotificationType.NewEntryForPost,
                        AdditionalInfo = entry.Context,
                        AdditionalId = entry.EntryId,
                        IsRead = false,
                        DateTime = DateTime.UtcNow,
                    };
                    postOwnerUser.ReceivedNotifications.Add(postOwnerNotification);
                    entryCreatorUser.SentNotifications.Add(postOwnerNotification);
                    await _genericCommandHandler.SaveChangesAsync();
                    mailEvents.AddRange(_mailEventFactory.CreateMailEvents(entryCreatorUser, null, new List<int?> { post.OwnerUserId }, MailType.NewEntryForPost, entry.Context, entry.EntryId));
                    notificationEvents.AddRange(_notificationEventFactory.CreateNotificationEvents(entryCreatorUser, null, new List<int?> { post.OwnerUserId }, NotificationType.NewEntryForPost, entry.Context, entry.EntryId));
                }

                mailEvents.AddRange(_mailEventFactory.CreateMailEvents(entryCreatorUser, null, toUserIds, MailType.CreatingEntry, entry.Context, entry.EntryId));
                notificationEvents.AddRange(_notificationEventFactory.CreateNotificationEvents(entryCreatorUser, null, toUserIds, NotificationType.CreatingEntry, entry.Context, entry.EntryId));
                await _queueSender.MailQueueSendAsync(mailEvents);
                await _queueSender.NotificationQueueSendAsync(notificationEvents);
                return IdentityResult.Success;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public override async Task<IdentityResult> DeleteEntryAsync(int userId, int entryId)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entry = await _entryQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(e => e.EntryId == entryId && e.OwnerUserId == userId).Include(e => e.OwnerUser).Include(e => e.Post));
                if (entry == null)
                    return IdentityResult.Failed(new NotFoundError("Entry not found or owner is not you"));
                if (entry.OwnerUser == null)
                    return IdentityResult.Failed(new NotFoundError("Owner User not found"));
                if (entry.Post == null)
                    return IdentityResult.Failed(new NotFoundError("Post not found"));

                entry.OwnerUser.EntryCount--;
                entry.Post.EntryCount--;
                await _genericCommandHandler.DeleteAsync<Entry>(entry);
                await _genericCommandHandler.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return IdentityResult.Success;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public override async Task<IdentityResult> EditEntryAsync(int userId, EditEntryDto editEntryDto)
        {
            var entry = await _entryQueryHandler.GetBySpecificPropertySingularAsync(q => q.Where(e => e.EntryId == editEntryDto.EntryId && e.OwnerUserId == userId).Include(e => e.OwnerUser));
            if (entry == null)
                return IdentityResult.Failed(new NotFoundError("Entry not found or owner is not you"));
            if (entry.OwnerUserId == null)
                return IdentityResult.Failed(new UnauthorizedError("Entry owner is not found"));

            entry = editEntryDto.Update___EditEntryDto_To_Entry(entry);
            await _genericCommandHandler.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public override async Task<ObjectIdentityResult<EntryProfileDto>> GetEntryAsync(int entryId)
        {
            var entry = await _entryQueryHandler.GetEntryModuleAsync(entryId);
            if (entry == null)
                return ObjectIdentityResult<EntryProfileDto>.Failed(null, new[] { new NotFoundError("Entry not found") });

            var entryProfileDto = entry.Entry_To_EntryProfileDto();
            return ObjectIdentityResult<EntryProfileDto>.Succeded(entryProfileDto);
        }

        public override async Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadEntryLikes(int entryId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var likes = await _likeQueryHandler.GetLikeModulesForEntryAsync(entryId, startInterval, endInterval);
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            foreach (var like in likes)
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return ObjectIdentityResult<List<MinimalLikeDto>>.Succeded(minimalLikeDtos);
        }
    }
}
