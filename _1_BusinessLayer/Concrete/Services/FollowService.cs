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
using _2_DataAccessLayer.Concrete.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Services
{
    public class FollowService : AbstractFollowService
    {
        public FollowService(AbstractFollowRepository followRepository, AbstractUserRepository userRepository, AbstractBotRepository botRepository, 
            MailEventFactory mailEventFactory, NotificationEventFactory notificationEventFactory, QueueSender queueSender, UnitOfWork unitOfWork) 
            : base(followRepository, userRepository, botRepository, mailEventFactory, notificationEventFactory, queueSender,unitOfWork)
        {
        }

        public override async Task<IdentityResult> DeleteFollow(int userId, int followId)
        {
            // Using multiple Savechanges() due to protect modularity of delete and manual insert-range methods in repository layer.
            // Cannot use delete operation with ef entity references directly due to need of including bloated related entities to server.
            // Using transaction due to multiple Savechanges() in a single process.
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var follow = await _followRepository.GetBySpecificPropertySingularAsync(
                    q => q.Where(f => f.FollowId == followId)
                          .Include(f => f.UserFollowed)
                          .Include(f => f.BotFollowed)
                          .Include(f => f.BotFollower)
                          .Include(f => f.UserFollower));

                if (follow == null)
                    return IdentityResult.Failed(new NotFoundError("Follow not found"));
                if (userId != follow.UserFollowerId && userId != follow.UserFollowedId)
                    return IdentityResult.Failed(new ForbiddenError("You are not allowed to delete this follow"));

                // Güncellemeleri ve silmeyi transaction içinde yap
                if (follow.UserFollowerId == userId && follow.UserFollower != null)
                {
                    if (follow.BotFollowed != null)
                    {
                        follow.BotFollowed.FollowerCount -= 1;
                        follow.UserFollower.FollowedCount -= 1;
                    }
                    else if (follow.UserFollowed != null)
                    {
                        follow.UserFollowed.FollowerCount -= 1;
                        follow.UserFollower.FollowedCount -= 1;
                    }
                }
                else if (follow.UserFollowedId == userId && follow.UserFollowed != null)
                {
                    if (follow.BotFollower != null)
                    {
                        follow.BotFollower.FollowedCount -= 1;
                        follow.UserFollowed.FollowerCount -= 1;
                    }
                    else if (follow.UserFollower != null)
                    {
                        follow.UserFollower.FollowedCount -= 1;
                        follow.UserFollowed.FollowerCount -= 1;
                    }
                }

                await _followRepository.DeleteAsync(follow);
                await _followRepository.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return IdentityResult.Success;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }


        public override async Task<IdentityResult> FollowBot(int userId, int followedBotId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new NotFoundError("FollowerUser not found"));
            var bot = await _botRepository.GetByIdAsync(followedBotId);
            if (bot == null) return IdentityResult.Failed(new NotFoundError("FollowedBot not found"));
            var follow = new Follow
            {
                UserFollowerId = userId,
                BotFollowedId = followedBotId,
            };
            user.Followed.Add(follow);
            bot.Followers.Add(follow);
            user.FollowedCount += 1;
            bot.FollowerCount += 1;
            bot.Activities.Add(new BotActivity
            {
                OwnerBotId = bot.Id,
                BotActivityType = BotActivityType.BotGainedFollower,
                AdditionalId = userId,
                AdditionalInfo = user.ProfileName,
                IsRead = false,
                DateTime = DateTime.UtcNow,
            });
            await _followRepository.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> FollowUser(int userId, int followedUserId)
        {
            if(userId == followedUserId) return IdentityResult.Failed(new ForbiddenError("You cannot follow yourself"));
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new NotFoundError("FollowerUser not found"));
            var followedUser = await _userRepository.GetByIdAsync(followedUserId);
            if (followedUser == null) return IdentityResult.Failed(new NotFoundError("FollowedUser not found"));          
            var follow = new Follow
            {
                UserFollowerId = userId,
                UserFollowedId = followedUserId,
            };
            user.Followed.Add(follow);
            followedUser.Followers.Add(follow);
            user.FollowedCount += 1;
            followedUser.FollowerCount += 1;
            var notification = new Notification
            {
                FromUserId = userId,
                OwnerUserId = followedUserId,
                NotificationType = NotificationType.GainedFollower,
                AdditionalId = userId,
                AdditionalInfo = user.ProfileName,
                IsRead = false,
                DateTime = DateTime.UtcNow,
            };
            user.SentNotifications.Add(notification);
            followedUser.ReceivedNotifications.Add(notification);
            await _followRepository.SaveChangesAsync();
            var notificationEvents = _notificationEventFactory.CreateNotificationEvents(user, null, new List<int?>{ followedUserId }, NotificationType.GainedFollower, user.ProfileName, userId);
            var mailEvents = _mailEventFactory.CreateMailEvents(user, null, new List<int?> { followedUserId }, MailType.GainedFollower, user.ProfileName, userId);
            await _queueSender.MailQueueSendAsync(mailEvents);
            await _queueSender.NotificationQueueSendAsync(notificationEvents);
            return IdentityResult.Success;
        }
    }
}
