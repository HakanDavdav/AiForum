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
using Microsoft.AspNetCore.Identity;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Services
{
    public class FollowService : AbstractFollowService
    {
        public FollowService(AbstractFollowRepository followRepository, AbstractUserRepository userRepository, AbstractBotRepository botRepository, 
            MailEventFactory mailEventFactory, NotificationEventFactory notificationEventFactory, QueueSender queueSender) 
            : base(followRepository, userRepository, botRepository, mailEventFactory, notificationEventFactory, queueSender)
        {
        }

        public override async Task<IdentityResult> DeleteFollow(int userId, int followId)
        {
            var follow = await _followRepository.GetByIdAsync(followId);
            if (follow == null) return IdentityResult.Failed(new NotFoundError("Follow not found"));
            if (follow.UserFollowerId != null && follow.UserFollowerId == userId)
            {
                var mainUser = await _userRepository.GetByIdAsync(userId);
                if (follow.BotFollowedId != null)
                {
                   await _followRepository.DeleteAsync(follow);
                   var bot = await _botRepository.GetByIdAsync(follow.BotFollowedId.Value);
                   bot.FollowerCount -= 1;
                   mainUser.FollowedCount -= 1;
                   await _botRepository.SaveChangesAsync();
                   return IdentityResult.Success;

                }
                else if (follow.UserFollowedId != null)
                {
                    await _followRepository.DeleteAsync(follow);
                    var user = await _userRepository.GetByIdAsync(follow.UserFollowedId.Value);
                    user.FollowerCount -= 1;
                    mainUser.FollowedCount -= 1;
                    await _userRepository.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnexpectedError("Invalid Follow object"));
            }
            else if (follow.UserFollowedId != null && follow.UserFollowedId == userId)
            {
                var mainUser = await _userRepository.GetByIdAsync(userId);
                if(follow.BotFollowerId != null)
                {
                    await _followRepository.DeleteAsync(follow);
                    var bot = await _botRepository.GetByIdAsync(follow.BotFollowerId.Value);
                    bot.FollowedCount -= 1;
                    mainUser.FollowerCount -= 1;
                    await _botRepository.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                else if(follow.UserFollowerId != null)
                {
                    await _followRepository.DeleteAsync(follow);
                    var user = await _userRepository.GetByIdAsync(follow.UserFollowerId.Value);
                    user.FollowedCount -= 1;
                    mainUser.FollowerCount -= 1;
                    await _userRepository.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnexpectedError("Invalid Follow object"));
            }
            return IdentityResult.Failed(new UnexpectedError("Invalid Follow object"));
        }

        public override async Task<IdentityResult> FollowBot(int userId, int followedBotId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
            var bot = await _botRepository.GetByIdAsync(followedBotId);
            if (bot == null) return IdentityResult.Failed(new NotFoundError("OwnerBot not found"));
            var follow = new Follow
            {
                UserFollowerId = userId,
                BotFollowedId = followedBotId,
            };
            user.Followed.Add(follow);
            bot.Followers.Add(follow);
            user.FollowedCount += 1;
            bot.FollowerCount += 1;
            await _followRepository.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> FollowUser(int userId, int followedUserId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
            var followedUser = await _userRepository.GetByIdAsync(followedUserId);
            if (followedUser == null) return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
            var follow = new Follow
            {
                UserFollowerId = userId,
                UserFollowedId = followedUserId,
            };
            user.Followed.Add(follow);
            followedUser.Followers.Add(follow);
            user.FollowedCount += 1;
            followedUser.FollowerCount += 1;
            followedUser.Notifications.Add(new Notification
            {
                FromUserId = userId,
                OwnerUserId = followedUserId,
                NotificationType = NotificationType.GainedFollower,
                AdditionalId = userId,
                IsRead = false,
                DateTime = DateTime.UtcNow,
            });
            await _followRepository.SaveChangesAsync();
            var notificationEvents = _notificationEventFactory.CreateNotificationEvents(user, null, new List<int?>{ followedUserId }, NotificationType.GainedFollower, user.ProfileName, userId);
            var mailEvents = _mailEventFactory.CreateMailEvents(user, null, new List<int?> { followedUserId }, MailType.GainedFollower, user.ProfileName, userId);
            await _queueSender.MailQueueSendAsync(mailEvents);
            await _queueSender.NotificationQueueSendAsync(notificationEvents);
            return IdentityResult.Success;
        }
    }
}
