using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Tools.Managers.UserToolManagers
{
    public class ActivityBaseManager
    {
        public readonly AbstractActivityRepository _activityRepository;
        public readonly AbstractNotificationRepository _notificationRepository;
        public readonly AbstractUserRepository _userRepository;

        public ActivityBaseManager(AbstractActivityRepository activityRepository, AbstractNotificationRepository notificationRepository, AbstractUserRepository userRepository)
        {
            _activityRepository = activityRepository;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;

        }
        public async Task<IdentityResult> ClearNotifications(User user)
        {
          var notifications = await _notificationRepository.GetWithCustomSearchAsync(query => query.Where(notification => notification.UserId == user.Id));
          foreach (var notification in notifications)
          {
              await _notificationRepository.DeleteAsync(notification);
          }
          return IdentityResult.Success;
        }

        public async Task<IdentityResult> CreateBotActivityAsync(Bot bot, ActivityType type, string additionalInfo, int additionalId)
        {
            var activityContext = string.Empty;
            if (bot != null)
            {
                if (type == ActivityType.CreatingEntry)
                {
                    activityContext = "A new " + additionalInfo + " entry has been created by the " + bot.BotProfileName + ".";
                }
                else if (type == ActivityType.CreatingPost)
                {
                    activityContext = "A new " + additionalInfo + " has been created by the " + bot.BotProfileName + ".";
                }
                else if (type == ActivityType.EntryLike)
                {
                    activityContext = "The " + bot.BotProfileName + " liked " + additionalInfo + " entry.";
                }
                else if (type == ActivityType.PostLike)
                {
                    activityContext = "The " + bot.BotProfileName + " liked " + additionalInfo + " post.";
                }
                else if (type == ActivityType.StartingFollow)
                {
                    activityContext = "The " + bot.BotProfileName + " started following " + additionalInfo + ".";
                }
                else if (type == ActivityType.GainingFollower)
                {
                    activityContext = "The " + bot.BotProfileName + " gained a new follower: " + additionalInfo + ".";
                }
                else
                {
                    return IdentityResult.Failed(new NotFoundError("Activity type not found"));
                }

                BotActivity activity = new BotActivity
                {
                    Bot = bot,
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

        public async Task<IdentityResult> CreateNotificationAsync(User? FromUser, Bot? FromBot, User toUser, NotificationType type, string additionalInfo, int additionalId)
        {
            var notificationContext = string.Empty;

            if (FromUser != null)
            {
                if (type == NotificationType.EntryLike)
                {
                    notificationContext = FromUser.UserName + " liked your " + additionalInfo + " entry ";
                }
                else if (type == NotificationType.PostLike)
                {
                    notificationContext = FromUser.UserName + " liked your " + additionalInfo + "post";
                }
                else if (type == NotificationType.CreatingEntry)
                {
                    notificationContext = FromUser.UserName + " created a new " + additionalInfo + " entry.";
                }
                else if (type == NotificationType.CreatingPost)
                {
                    notificationContext = FromUser.UserName + " created a new " + additionalInfo + " post.";
                }
                else if (type == NotificationType.GainedFollower)
                {
                    notificationContext = FromUser.UserName + " started following you.";
                }
                else if (type == NotificationType.NewEntryForPost)
                {
                    notificationContext = FromUser.UserName + " added a new entry to your " + additionalInfo + "Post";
                }
                else
                {
                    return IdentityResult.Failed(new NotFoundError("Notification type not found"));
                }
                await _notificationRepository.ManuallyInsertAsync(new Notification
                {
                    AdditionalId = additionalId,
                    FromUser = FromUser,
                    User = toUser,
                    NotificationType = type,
                    NotificationContext = notificationContext,
                    DateTime = DateTime.UtcNow,
                    IsRead = false,
                    ImageUrl = FromUser.ImageUrl,
                    Title = "New Notification",
                }
                );
                await _notificationRepository.SaveChangesAsync();
                return IdentityResult.Success;
            }

            else if (FromBot != null)
            {
                if (type == NotificationType.PostLike)
                {
                    notificationContext = FromBot.BotProfileName + " liked your " + additionalInfo + "post";
                }
                else if (type == NotificationType.EntryLike)
                {
                    notificationContext = FromBot.BotProfileName + " liked your " + additionalInfo + " entry ";
                }
                else if (type == NotificationType.CreatingEntry)
                {
                    notificationContext = FromBot.BotProfileName + " created a new " + additionalInfo + " entry.";
                }
                else if (type == NotificationType.CreatingPost)
                {
                    notificationContext = FromBot.BotProfileName + " created a new " + additionalInfo + " post.";
                }
                else if (type == NotificationType.GainedFollower)
                {
                    notificationContext = FromBot.BotProfileName + " started following you.";
                }
                else if (type == NotificationType.NewEntryForPost)
                {
                    notificationContext = FromBot.BotProfileName + " added a new entry to your " + additionalInfo + " post " ;
                }
                else
                {
                    return IdentityResult.Failed(new NotFoundError("Notification type not found"));
                }

                await _notificationRepository.ManuallyInsertAsync(new Notification
                {
                    AdditionalId = additionalId,
                    FromBot = FromBot,
                    User = toUser,
                    NotificationType = type,
                    NotificationContext = notificationContext,
                    DateTime = DateTime.UtcNow,
                    IsRead = false,
                    ImageUrl = FromBot.ImageUrl,
                    Title = "New Notification",
                });

                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new NotFoundError("Sender not found"));
            }
        }

        public a
    }
}
