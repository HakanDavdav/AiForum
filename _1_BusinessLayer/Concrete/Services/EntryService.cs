using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
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
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null) return IdentityResult.Failed(new NotFoundError("Post not found"));
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new NotFoundError("ParentUser not found"));
            var entry = createEntryDto.CreateEntryDto_To_Entry(userId);
            var follows = await _followRepository.GetWithCustomSearchAsync(query => query.Where(follow => follow.UserFollowedId == userId).AsNoTracking());
            var toUserIds = follows.Select(follow => follow.UserFollowerId).ToList();
            var notifications = new List<Notification>();
            foreach (var toUserId in toUserIds)
            {
                notifications.Add(new Notification
                {
                    FromUserId = userId,
                    OwnerUserId = toUserId,
                    NotificationType = NotificationType.CreatingEntry,
                    AdditionalId = entry.EntryId,
                    IsRead = false,
                    DateTime = DateTime.UtcNow,
                });
            }
            await _notificationRepository.ManuallyInsertRangeAsync(notifications);
            post.Entries.Add(entry);
            user.Entries.Add(entry);
            post.EntryCount += 1;
            user.EntryCount += 1;
            await _entryRepository.SaveChangesAsync();
            var mailEvents = _mailEventFactory.CreateMailEvents(user, null, toUserIds, MailType.CreatingEntry, entry.Context, entry.EntryId);
            var notificationEvents = _notificationEventFactory.CreateNotificationEvents(user, null, toUserIds, NotificationType.CreatingEntry, entry.Context, entry.EntryId);
            await _queueSender.MailQueueSendAsync(mailEvents);
            await _queueSender.NotificationQueueSendAsync(notificationEvents);
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeleteEntryAsync(int userId, int entryId)
        {
            var entry = await _entryRepository.GetByIdAsync(entryId);
            if (entry == null) return IdentityResult.Failed(new NotFoundError("Entry not found"));
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new NotFoundError("ParentUser not found"));
            var post = await _postRepository.GetByIdAsync(entry.PostId);
            if (post == null) return IdentityResult.Failed(new NotFoundError("Post not found"));
            if (entry.OwnerUserId == user.Id)
            {
                await _entryRepository.DeleteAsync(entry);
                user.EntryCount--;
                post.EntryCount--;
                await _entryRepository.SaveChangesAsync();
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("You cannot delete another user's entry"));
        }

        public override async Task<IdentityResult> EditEntryAsync(int userId, EditEntryDto editEntryDto)
        {
            var entry = await _entryRepository.GetByIdAsync(editEntryDto.EntryId);
            if (entry != null)
            {
                if (entry.OwnerUserId == userId)
                {
                    entry = editEntryDto.Update___EditEntryDto_To_Entry(entry);
                    await _entryRepository.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnauthorizedError("You cannot edit another user's entry"));
            }
            return IdentityResult.Failed(new UnauthorizedError("You cannot edit another user's entry"));
        }

        public override async Task<ObjectIdentityResult<EntryProfileDto>> GetEntryAsync(int entryId)
        {
            var entry = await _entryRepository.GetEntryModuleAsync(entryId);
            if (entry != null)
            {
                var entryProfileDto = entry.Entry_To_EntryProfileDto();
                return ObjectIdentityResult<EntryProfileDto>.Succeded(entryProfileDto);
            }
            return ObjectIdentityResult<EntryProfileDto>.Failed(null, new[] { new NotFoundError("Entry not found") });
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
