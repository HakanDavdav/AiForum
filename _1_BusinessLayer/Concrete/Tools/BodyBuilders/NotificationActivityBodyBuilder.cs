using System;
using _1_BusinessLayer.Concrete.Events;
using _2_DataAccessLayer.Concrete.Entities;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Tools.BodyBuilders
{
    public class NotificationActivityBodyBuilder
    {
        /// <summary>
        /// ParentBot aktiviteleri için title ve body oluşturur.
        /// </summary>
        public (string title, string body) BuildAppBotActivityContent(
            BotActivity botActivity)
        {
            string botName = botActivity.OwnerBot.BotProfileName ?? "A bot";

            string title = botActivity.BotActivityType switch
            {
                BotActivityType.BotCreatedEntry => $"{botName} created a new entry",
                BotActivityType.BotCreatedPost => $"{botName} created a new post",
                BotActivityType.BotPostLiked => $"{botName}'s post has liked",
                BotActivityType.BotEntryLiked => $"{botName}'s entry has liked",
                BotActivityType.BotLikedEntry => $"{botName} liked an entry",
                BotActivityType.BotLikedPost => $"{botName} liked a post",
                BotActivityType.BotStartedFollow => $"{botName} started following someone",
                BotActivityType.BotGainedFollower => $"{botName} gained a new follower",
                _ => "ParentBot Activity"
            };

            string body = botActivity.BotActivityType switch
            {
                BotActivityType.BotPostLiked => $"{botName}'s post liked by {botActivity.AdditionalInfo}.",
                BotActivityType.BotEntryLiked => $"{botName}'s post liked by {botActivity.AdditionalInfo}.",
                BotActivityType.BotCreatedEntry => $"A new {botActivity.AdditionalInfo} entry has been created by {botName}.",
                BotActivityType.BotCreatedPost => $"A new {botActivity.AdditionalInfo} post has been created by {botName}.",
                BotActivityType.BotLikedEntry => $"{botName} liked {botActivity.AdditionalInfo} entry.",
                BotActivityType.BotLikedPost => $"{botName} liked {botActivity.AdditionalInfo} post.",
                BotActivityType.BotStartedFollow => $"{botName} started following: {botActivity.AdditionalInfo}.",
                BotActivityType.BotGainedFollower => $"{botName} gained a new follower: {botActivity.AdditionalInfo}.",
                _ => "You have a new bot activity."
            };

            return (title, body);
        }


        public (string title, string body, string url) BuildWebPushNotificationContent(
            User? FromUser,
            Bot? FromBot,
            NotificationEvent notificationEvent
            )
        {
            string ProfileName = FromUser?.ProfileName ?? FromBot?.BotProfileName ?? "Someone";

            string title = notificationEvent.Type switch
            {
                NotificationType.EntryLike => $"{ProfileName} liked your entry",
                NotificationType.PostLike => $"{ProfileName} liked your post",
                NotificationType.CreatingEntry => $"{ProfileName} created a new entry",
                NotificationType.CreatingPost => $"{ProfileName} created a new post",
                NotificationType.GainedFollower => $"{ProfileName} started following you",
                NotificationType.NewEntryForPost => $"{ProfileName} added a new entry to your post",
                NotificationType.BotActivity => "You have a new bot activity.",
                _ => "Notification"
            };

            string body = notificationEvent.Type switch
            {
                NotificationType.EntryLike => $"{notificationEvent.AdditionalInfo} entry",
                NotificationType.PostLike => $"{notificationEvent.AdditionalInfo} post",
                NotificationType.CreatingEntry => $"{notificationEvent.AdditionalInfo} entry",
                NotificationType.CreatingPost => $"{notificationEvent.AdditionalInfo} post",
                NotificationType.GainedFollower => $"{notificationEvent.AdditionalInfo} user",
                NotificationType.NewEntryForPost => $"{notificationEvent.AdditionalInfo} entry",
                NotificationType.BotActivity => "You have a new bot activity.",
                _ => "You have a new notification"
            };

            string url = notificationEvent.Type switch
            {
                NotificationType.EntryLike => $"https://example.com/entry/{notificationEvent.AdditionalId}",
                NotificationType.PostLike => $"https://example.com/post/{notificationEvent.AdditionalId}",
                NotificationType.CreatingEntry => $"https://example.com/entry/{notificationEvent.AdditionalId}",
                NotificationType.CreatingPost => $"https://example.com/post/{notificationEvent.AdditionalId}",
                NotificationType.GainedFollower => $"https://example.com/user/{notificationEvent.AdditionalId}",
                NotificationType.NewEntryForPost => $"https://example.com/entry/{notificationEvent.AdditionalId}",
                NotificationType.BotActivity => $"",
                _ => "https://example.com"
            };

            return (title, body, url);
        }

        public (string title, string body) BuildAppNotificationContent(
            Notification notification)
        {
            string profileName = notification.FromUser?.UserName ?? notification.FromBot?.BotProfileName ?? "Someone";

            string title = notification.NotificationType switch
            {
                NotificationType.EntryLike => $"{profileName} liked your entry",
                NotificationType.PostLike => $"{profileName} liked your post",
                NotificationType.CreatingEntry => $"{profileName} created a new entry",
                NotificationType.CreatingPost => $"{profileName} created a new post",
                NotificationType.GainedFollower => $"{profileName} started following you",
                NotificationType.NewEntryForPost => $"{profileName} added a new entry to your post",
                NotificationType.BotActivity => "You have a new bot activity.",
                _ => "Notification"
            };

            string body = notification.NotificationType switch
            {
                NotificationType.EntryLike => $"{notification.AdditionalInfo} entry",
                NotificationType.PostLike => $"{notification.AdditionalInfo} post",
                NotificationType.CreatingEntry => $"{notification.AdditionalInfo} entry",
                NotificationType.CreatingPost => $"{notification.AdditionalInfo} post",
                NotificationType.GainedFollower => $"{notification.AdditionalInfo} user",
                NotificationType.NewEntryForPost => $"{notification.AdditionalInfo} entry",
                NotificationType.BotActivity => "You have a new bot activity.",
                _ => "You have a new notification"
            };
            return (title, body);

        }
    }
}
