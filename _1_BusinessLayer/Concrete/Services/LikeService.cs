using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.Factories;
using _1_BusinessLayer.Concrete.Tools.MessageBackgroundService;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Services
{
    public class LikeService : AbstractLikeService
    {
        public LikeService(AbstractLikeRepository likeRepository, AbstractUserRepository userRepository, AbstractPostRepository postRepository, AbstractEntryRepository entryRepository, MailEventFactory mailEventFactory, AbstractNotificationRepository notificationRepository, NotificationEventFactory notificationEventFactory, QueueSender queueSender, AbstractFollowRepository followRepository, AbstractBotRepository botRepository) : base(likeRepository, userRepository, postRepository, entryRepository, mailEventFactory, notificationRepository, notificationEventFactory, queueSender, followRepository, botRepository)
        {
        }

        public override async Task<IdentityResult> LikeEntry(int entryId, int userId)
        {
            object entryOwner = null;
            var entry = await _entryRepository.GetByIdAsync(userId);
            if (entry == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));
            {
                if (entry.OwnerUserId.HasValue)
                    entryOwner = await _userRepository.GetByIdAsync(userId);

                else if (entry.OwnerBotId.HasValue)
                    entryOwner = await _botRepository.GetByIdAsync(userId);
            }

            var likerUser = await _userRepository.GetByIdAsync(userId);


            if (entryOwner == null)
                return IdentityResult.Failed(new NotFoundError("Entry Owner not found"));

            if (likerUser == null)
                return IdentityResult.Failed(new NotFoundError("Liker user not found"));


            {
                if (entryOwner is User entryOwnerUser)
                {
                    var like = new Like
                    {
                        EntryId = entryId,
                        OwnerUserId = userId,
                    };
                    likerUser.Likes.Add(like);
                    entry.Likes.Add(like);
                    likerUser.LikeCount += 1;
                    var notification = new Notification
                    {
                        NotificationType = NotificationType.EntryLike,
                        AdditionalId = entry.EntryId,
                        AdditionalInfo = entry.Context.Substring(0, 10) + "...",
                        FromUserId = likerUser.Id,
                        OwnerUserId = entryOwnerUser.Id,
                        DateTime = DateTime.UtcNow,
                        IsRead = false
                    };
                    entryOwnerUser.ReceivedNotifications.Add(notification);
                    likerUser.SentNotifications.Add(notification);
                    await _likeRepository.SaveChangesAsync();
                    var notificationEvents = _notificationEventFactory.CreateNotificationEvents(likerUser, null, new List<int?> { entryOwnerUser.Id }, NotificationType.EntryLike, entry.Context, entry.EntryId);
                    var mailEvents = _mailEventFactory.CreateMailEvents(likerUser, null, new List<int?> { entryOwnerUser.Id }, MailType.EntryLike, entry.Context, entry.EntryId);
                    await _queueSender.NotificationQueueSendAsync(notificationEvents);
                    await _queueSender.MailQueueSendAsync(mailEvents);
                    return IdentityResult.Success;
                }
                else if (entryOwner is Bot entryOwnerBot)
                {
                    var like = new Like
                    {
                        EntryId = entryId,
                        OwnerUserId = userId,
                    };
                    likerUser.Likes.Add(like);
                    entry.Likes.Add(like);
                    likerUser.LikeCount += 1;
                    entry.LikeCount += 1;
                    entryOwnerBot.Activities.Add(new BotActivity
                    {
                        BotActivityType = BotActivityType.BotEntryLiked,
                        AdditionalId = entry.EntryId,
                        AdditionalInfo = likerUser.ProfileName,
                        OwnerBotId = entryOwnerBot.Id,
                        IsRead = false,
                        DateTime = DateTime.UtcNow
                    });
                    await _likeRepository.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(new UnexpectedError("Entry Owner type not found"));
                }
            }
        }



        public override async Task<IdentityResult> LikePost(int postId, int userId)
        {
            object postOwner = null;
            var post = await _postRepository.GetByIdAsync(userId);
            if (post == null)
                return IdentityResult.Failed(new NotFoundError("Post not found"));

            {
                if (post.OwnerBotId.HasValue)
                    postOwner = await _botRepository.GetByIdAsync(userId);

                else if (post.OwnerUserId.HasValue)
                    postOwner = await _userRepository.GetByIdAsync(userId);
            }
            var likerUser = await _userRepository.GetByIdAsync(userId);

            if (postOwner == null)
                return IdentityResult.Failed(new NotFoundError("Post Owner not found"));
            if (likerUser == null)
                return IdentityResult.Failed(new NotFoundError("Liker user not found"));
            {
                if (postOwner is User postOwnerUser)
                {
                    var like = new Like
                    {
                        PostId = postId,
                        OwnerUserId = userId,
                    };
                    likerUser.Likes.Add(like);
                    post.Likes.Add(like);
                    likerUser.LikeCount += 1;
                    post.LikeCount += 1;
                    var notification = new Notification
                    {
                        NotificationType = NotificationType.PostLike,
                        AdditionalId = post.PostId,
                        AdditionalInfo = post.Title,
                        FromUserId = likerUser.Id,
                        OwnerUserId = postOwnerUser.Id,
                        DateTime = DateTime.UtcNow,
                        IsRead = false
                    };
                    postOwnerUser.ReceivedNotifications.Add(notification);
                    likerUser.SentNotifications.Add(notification);
                    await _likeRepository.SaveChangesAsync();
                    var notificationEvents = _notificationEventFactory.CreateNotificationEvents(likerUser, null, new List<int?> { postOwnerUser.Id }, NotificationType.PostLike, post.Title, post.PostId);
                    var mailEvents = _mailEventFactory.CreateMailEvents(likerUser, null, new List<int?> { postOwnerUser.Id }, MailType.PostLike, post.Title, post.PostId);
                    await _queueSender.NotificationQueueSendAsync(notificationEvents);
                    await _queueSender.MailQueueSendAsync(mailEvents);
                    return IdentityResult.Success;
                }
                else if (postOwner is Bot postOwnerBot)
                {
                    var like = new Like
                    {
                        PostId = postId,
                        OwnerUserId = userId,
                    };
                    likerUser.Likes.Add(like);
                    post.Likes.Add(like);
                    likerUser.LikeCount += 1;
                    post.LikeCount += 1;
                    postOwnerBot.Activities.Add(new BotActivity
                    {
                        BotActivityType = BotActivityType.BotPostLiked,
                        AdditionalId = post.PostId,
                        AdditionalInfo = likerUser.ProfileName,
                        OwnerBotId = postOwnerBot.Id,
                        IsRead = false,
                        DateTime = DateTime.UtcNow
                    });
                    await _likeRepository.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(new UnexpectedError("Post Owner type not found"));
                }

            }

        }

        public override async Task<IdentityResult> UnlikeEntry(int userId, int likeId)
        {
            //Due to the need of updating relational entities and need of cross id validation we need to include them here with custom repo method instead of using module methods.
            var like = await _likeRepository.GetBySpecificPropertySingularAsync(q => q.Where(l => l.LikeId == likeId).Include(l => l.Entry).Include(l => l.OwnerUser));
            if (like == null)
                return IdentityResult.Failed(new NotFoundError("Like not found"));
            if (like.Entry == null)
                return IdentityResult.Failed(new NotFoundError("Entry not found"));
            if (like.OwnerUser == null)
                return IdentityResult.Failed(new NotFoundError("User not found"));

            await _likeRepository.DeleteAsync(like);
            like.Entry.LikeCount--;
            like.OwnerUser.LikeCount--;
            await _likeRepository.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> UnlikePost(int userId, int likeId)
        {
            //Due to the need of updating relational entities and need of cross id validation we need to include them here with custom repo method instead of using module methods.
            var like = await _likeRepository.GetBySpecificPropertySingularAsync(q => q.Where(l => l.LikeId == likeId).Include(l => l.Post).Include(l => l.OwnerUser));
            if (like == null)
                return IdentityResult.Failed(new NotFoundError("Like not found"));
            if (like.Post == null)
                return IdentityResult.Failed(new NotFoundError("Post not found"));
            if (like.OwnerUser == null)
                return IdentityResult.Failed(new UnauthorizedError("Like owner user not found"));
            var user = await _userRepository.GetByIdAsync(userId);

            await _likeRepository.DeleteAsync(like);
            like.Post.LikeCount--;
            like.OwnerUser.LikeCount--;
            await _likeRepository.SaveChangesAsync();
            return IdentityResult.Success;
        }
    }

    }
