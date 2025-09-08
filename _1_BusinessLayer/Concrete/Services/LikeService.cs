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
            if (entry != null)
            {
                if (entry.OwnerUserId.HasValue) entryOwner = await _userRepository.GetByIdAsync(userId);
                if (entry.OwnerBotId.HasValue) entryOwner = await _botRepository.GetByIdAsync(userId);
                if (entryOwner != null)
                {
                    var likerUser = await _userRepository.GetByIdAsync(userId);
                    if (likerUser != null)
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
                        return IdentityResult.Failed(new NotFoundError("Entry Owner type not found"));
                    }
                    return IdentityResult.Failed(new NotFoundError("Entry liker not found"));
                }
                return IdentityResult.Failed(new NotFoundError("Entry Owner not found"));
            }
            return IdentityResult.Failed(new NotFoundError("Entry not found"));
        }

        public override async Task<IdentityResult> LikePost(int postId, int userId)
        {
            object postOwner = null;
            var post = await _postRepository.GetByIdAsync(userId);
            if (post != null)
            {
                if (post.OwnerBotId.HasValue) postOwner = await _botRepository.GetByIdAsync(userId);
                if (post.OwnerUserId.HasValue) postOwner = await _userRepository.GetByIdAsync(userId);
                if (postOwner != null)
                {
                    var likerUser = await _userRepository.GetByIdAsync(userId);
                    if (likerUser != null)
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
                        return IdentityResult.Failed(new NotFoundError("Post Owner type not found"));
                    }
                    return IdentityResult.Failed(new NotFoundError("Post liker not found"));
                }
                return IdentityResult.Failed(new NotFoundError("Post Owner not found"));
            }
            return IdentityResult.Failed(new NotFoundError("Post not found"));



        }

        public override async Task<IdentityResult> UnlikeEntry(int userId, int likeId)
        {
            var like = await _likeRepository.GetByIdAsync(likeId);
            if (like != null)
            {
                if (like.EntryId != null)
                {
                    var entry = await _entryRepository.GetByIdAsync(likeId);
                    if (entry != null)
                    {
                        if (entry.EntryId == like.EntryId)
                        {
                            var user = await _userRepository.GetByIdAsync(likeId);
                            if (user != null)
                            {
                                await _likeRepository.DeleteAsync(like);
                                entry.LikeCount--;
                                user.LikeCount--;
                                await _likeRepository.SaveChangesAsync();
                                return IdentityResult.Success;
                            }
                            return IdentityResult.Failed(new NotFoundError("User not found "));
                        }
                        return IdentityResult.Failed(new UnauthorizedError("Entry doesen't have that like "));
                    }
                    return IdentityResult.Failed(new UnauthorizedError("Entry not found "));
                }
                return IdentityResult.Failed(new UnauthorizedError("Like is not for an entry"));
            }
            return IdentityResult.Failed(new NotFoundError("Like not found"));
        }

        public override async Task<IdentityResult> UnlikePost(int userId, int likeId)
        {
            var like = await _likeRepository.GetByIdAsync(likeId);
            if (like != null)
            {
                if (like.PostId != null)
                {
                    var post = await _postRepository.GetByIdAsync(likeId);
                    if (post != null)
                    {
                        if (post.PostId == like.PostId)
                        {
                            var user = await _userRepository.GetByIdAsync(likeId);
                            if (user != null)
                            {
                                await _likeRepository.DeleteAsync(like);
                                post.LikeCount--;
                                user.LikeCount--;
                                await _likeRepository.SaveChangesAsync();
                                return IdentityResult.Success;
                            }
                            return IdentityResult.Failed(new NotFoundError("User not found "));
                        }
                        return IdentityResult.Failed(new UnauthorizedError("Post doesen't have that like "));
                    }
                    return IdentityResult.Failed(new NotFoundError("Post not found "));
                }
              return IdentityResult.Failed(new NotFoundError("Like is not for an post"));
            }
            return IdentityResult.Failed(new NotFoundError("Like not found"));
        }
    }
}
