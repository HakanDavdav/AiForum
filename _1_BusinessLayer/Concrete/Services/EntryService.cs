using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Events;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _1_BusinessLayer.Concrete.Tools.MessageBackgroundService;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Services
{
    public class EntryService : AbstractEntryService
    {
        public EntryService(AbstractEntryRepository entryRepository, AbstractUserRepository userRepository, AbstractLikeRepository likeRepository,
            AbstractPostRepository postRepository, AbstractFollowRepository followRepository, MailEventFactory mailEventFactory,
            NotificationEventFactory notificationEventFactory, QueueSender queueSender, AbstractNotificationRepository notificationRepository)
            : base(entryRepository, userRepository, likeRepository, postRepository, followRepository, mailEventFactory, notificationEventFactory, queueSender, notificationRepository)
        {
        }

        public override async Task<IdentityResult> CreateEntryAsync(int userId, int postId, CreateEntryDto createEntryDto)
        {
            var post = await _postRepository.GetBySpecificPropertySingularAsync(q => q.Where(p => p.PostId == postId).Include(p => p.OwnerUser));
            if (post == null) return IdentityResult.Failed(new NotFoundError("Post not found"));
            var entryCreatorUser = await _userRepository.GetByIdAsync(userId);
            if (entryCreatorUser == null) return IdentityResult.Failed(new NotFoundError("User not found"));
            var entry = createEntryDto.CreateEntryDto_To_Entry();
            var follows = await _followRepository.GetWithCustomSearchAsync(query => query.Where(follow => follow.UserFollowedId == userId).AsNoTracking());
            var toUserIds = follows.Select(follow => follow.UserFollowerId).ToList();
            var creatorUserFollowerNotifications = new List<Notification>();
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
            //insert entry and creatorUserFollowerNotifications in a single transaction
            await _notificationRepository.ManuallyInsertRangeAsync(creatorUserFollowerNotifications);
            await _notificationRepository.ManuallyInsertAsync(postOwnerNotification);
            post.Entries.Add(entry);
            entryCreatorUser.Entries.Add(entry);
            post.EntryCount += 1;
            entryCreatorUser.EntryCount += 1;
            await _entryRepository.SaveChangesAsync();
            var followersMailEvents = _mailEventFactory.CreateMailEvents(entryCreatorUser, null, toUserIds, MailType.CreatingEntry, entry.Context, entry.EntryId);
            var followersNotificationEvents = _notificationEventFactory.CreateNotificationEvents(entryCreatorUser, null, toUserIds, NotificationType.CreatingEntry, entry.Context, entry.EntryId);
            var postOwnerMailEvent = _mailEventFactory.CreateMailEvents(entryCreatorUser, null, new List<int?> { post.OwnerUserId }, MailType.NewEntryForPost, entry.Context, entry.EntryId);
            var postOwnerNotificationEvent = _notificationEventFactory.CreateNotificationEvents(entryCreatorUser, null, new List<int?> { post.OwnerUserId }, NotificationType.NewEntryForPost, entry.Context, entry.EntryId);
            var mailEvents = new List<MailEvent>();
            var notificationEvents = new List<NotificationEvent>();
            mailEvents.AddRange(followersMailEvents);
            mailEvents.AddRange(postOwnerMailEvent);
            notificationEvents.AddRange(followersNotificationEvents);
            notificationEvents.AddRange(postOwnerNotificationEvent);
            await _queueSender.MailQueueSendAsync(mailEvents);
            await _queueSender.NotificationQueueSendAsync(notificationEvents);
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeleteEntryAsync(int userId, int entryId)
        {
            var entry = await _entryRepository.GetBySpecificPropertySingularAsync(q => q.Where(e => e.EntryId == entryId).Include(e => e.OwnerUser).Include(e => e.Post));
            if (entry == null)
                return IdentityResult.Failed(new NotFoundError("Entry not found"));
            if (entry.OwnerUser == null)
                return IdentityResult.Failed(new NotFoundError("Owner User not found"));
            if (entry.Post == null)
                return IdentityResult.Failed(new NotFoundError("Post not found"));

            await _entryRepository.DeleteAsync(entry);
            entry.OwnerUser.EntryCount--;
            entry.Post.EntryCount--;
            await _entryRepository.SaveChangesAsync();
            return IdentityResult.Success;

        }

        public override async Task<IdentityResult> EditEntryAsync(int userId, EditEntryDto editEntryDto)
        {
            var entry = await _entryRepository.GetByIdAsync(editEntryDto.EntryId);
            if (entry == null)
                return IdentityResult.Failed(new NotFoundError("Entry not found"));
            if (entry.OwnerUserId != userId)
                return IdentityResult.Failed(new UnauthorizedError("You cannot edit another entryCreatorUser's entry"));

            entry = editEntryDto.Update___EditEntryDto_To_Entry(entry);
            await _entryRepository.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public override async Task<ObjectIdentityResult<EntryProfileDto>> GetEntryAsync(int entryId)
        {
            var entry = await _entryRepository.GetEntryModuleAsync(entryId);
            if (entry == null)
                return ObjectIdentityResult<EntryProfileDto>.Failed(null, new[] { new NotFoundError("Entry not found") });

            var entryProfileDto = entry.Entry_To_EntryProfileDto();
            return ObjectIdentityResult<EntryProfileDto>.Succeded(entryProfileDto);
        }


        public override async Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadEntryLikes(int entryId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var likes = await _likeRepository.GetLikeModulesForEntry(entryId, startInterval, endInterval);
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            foreach (var like in likes)
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return ObjectIdentityResult<List<MinimalLikeDto>>.Succeded(minimalLikeDtos);
        }
    }
}
