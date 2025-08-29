using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class ActivityBaseService : AbstractActivityBaseService
    {
        public ActivityBaseService(AbstractActivityRepository activityRepository, AbstractNotificationRepository notificationRepository, AbstractUserRepository userRepository, AbstractBotRepository botRepository) : base(activityRepository, notificationRepository, userRepository, botRepository)
        {
        }

        public override async Task<IdentityResult> ClearNotifications(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var notifications = await _notificationRepository.GetWithCustomSearchAsync(query => query.Where(notification => notification.UserId == userId));
                foreach (var notification in notifications)
                {
                    await _notificationRepository.DeleteAsync(notification);
                }
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));

        }

        public override async Task<IdentityResult> CreateBotActivityAsync(int botId, ActivityType type, string additionalInfo, int additionalId)
        {
            var activityContext = string.Empty;
            var bot = await _botRepository.GetByIdAsync(botId);
            if (bot != null)
            {
                if (type == ActivityType.CreatingEntry)
                {
                    activityContext = "A new "+additionalInfo+" entry has been created by the " + bot.BotProfileName + ".";
                }
                else if (type == ActivityType.CreatingPost)
                {
                    activityContext = "A new "+additionalInfo+" has been created by the "+ bot.BotProfileName  +".";
                }
                else if (type == ActivityType.Like)
                {
                    activityContext = "The "+bot.BotProfileName+" liked "+ additionalInfo +" content.";
                }


                BotActivity activity = new BotActivity
                {
                    BotId = botId,
                    ActivityType = type,
                    DateTime = DateTime.UtcNow,
                    AdditionalId = additionalId,
                    ActivityContext = activityContext
                };
                bot.Activities.Add(activity);
                await _activityRepository.SaveChangesAsync();
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("Bot not found"));
        }

        public override async Task<IdentityResult> CreateNotificationAsync(User FromUser, Bot FromBot, List<User> ToUsers, NotificationType type, int additionalInfo, int additionalId)
        {
           var notificationContext = string.Empty;
            if (FromUser != null)
            {
                if (type == NotificationType.Like)
                {
                    notificationContext = FromUser.UserName + " liked your content.";
                }
                else if (type == NotificationType.CreatingEntry)
                {
                    notificationContext = FromUser.UserName + " created a new " + additionalInfo + " entry.";
                }
                else if (type == NotificationType.CreatingPost)
                {
                    notificationContext = FromUser.UserName + " created a new " + additionalInfo + " post.";
                }
                else if (type == NotificationType.FollowGain)
                {
                    notificationContext = FromUser.UserName + " started following you.";
                }
                foreach (var toUser in ToUsers)
                {
                    toUser.Notifications.Add(new Notification
                    {
                        FromUser = FromUser,
                        User = toUser,
                        NotificationType = type,
                        NotificationContext = notificationContext,
                        DateTime = DateTime.UtcNow,
                        AdditionalId = additionalId,
                        IsRead = false,
                        ImageUrl = FromUser.ImageUrl,
                        Title = "New Notification",
                    });
                    TOuS
                }
            }
           else if (FromBot != null)
            {

            }
        }

        public override Task<IdentityResult> MarkAsRead(int userId, int[] notificationIds)
        {
            throw new NotImplementedException();
        }
    }
}
