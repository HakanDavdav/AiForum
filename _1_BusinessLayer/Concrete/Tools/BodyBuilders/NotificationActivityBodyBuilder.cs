using System;
using _2_DataAccessLayer.Concrete.Entities;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Tools.BodyBuilders
{
    public class NotificationActivityBodyBuilder
    {
        /// <summary>
        /// OwnerBot aktiviteleri için title ve body oluşturur.
        /// </summary>
        public (string title, string body) BuildAppBotActivityContent(
            BotActivity botActivity)
        {
            string botName = botActivity.OwnerBot.BotProfileName ?? "A bot";

            string title = botActivity.BotActivityType switch
            {
                BotActivityType.BotCreatedEntry => $"{botName} created a new entry",
                BotActivityType.BotCreatedPost => $"{botName} created a new post",
                BotActivityType.BotLikedEntry => $"{botName} liked an entry",
                BotActivityType.BotLikedPost => $"{botName} liked a post",
                BotActivityType.BotStartedFollow => $"{botName} started following someone",
                BotActivityType.BotGainedFollower => $"{botName} gained a new follower",
                _ => "OwnerBot Activity"
            };

            string body = botActivity.BotActivityType switch
            {
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
            NotificationType type,
            string additionalInfo,
            int additionalId)
        {
            string ProfileName = FromUser?.UserName ?? FromBot?.BotProfileName ?? "Someone";

            string title = type switch
            {
                NotificationType.EntryLike => $"{ProfileName} liked your entry",
                NotificationType.PostLike => $"{ProfileName} liked your post",
                NotificationType.CreatingEntry => $"{ProfileName} created a new entry",
                NotificationType.CreatingPost => $"{ProfileName} created a new post",
                NotificationType.GainedFollower => $"{ProfileName} started following you",
                NotificationType.NewEntryForPost => $"{ProfileName} added a new entry to your post",
                _ => "Notification"
            };

            string body = type switch
            {
                NotificationType.EntryLike => $"{additionalInfo} entry",
                NotificationType.PostLike => $"{additionalInfo} post",
                NotificationType.CreatingEntry => $"{additionalInfo} entry",
                NotificationType.CreatingPost => $"{additionalInfo} post",
                NotificationType.GainedFollower => $"{additionalInfo} user",
                NotificationType.NewEntryForPost => $"{additionalInfo} post",
                _ => "You have a new notification"
            };

            string url = type switch
            {
                NotificationType.EntryLike => $"https://example.com/entry/{additionalId}",
                NotificationType.PostLike => $"https://example.com/post/{additionalId}",
                NotificationType.CreatingEntry => $"https://example.com/entry/{additionalId}",
                NotificationType.CreatingPost => $"https://example.com/post/{additionalId}",
                NotificationType.GainedFollower => $"https://example.com/user/{additionalId}",
                NotificationType.NewEntryForPost => $"https://example.com/post/{additionalId}",
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
                _ => "Notification"
            };

            string body = notification.NotificationType switch
            {
                NotificationType.EntryLike => $"{notification.AdditionalInfo} entry",
                NotificationType.PostLike => $"{notification.AdditionalInfo} post",
                NotificationType.CreatingEntry => $"{notification.AdditionalInfo} entry",
                NotificationType.CreatingPost => $"{notification.AdditionalInfo} post",
                NotificationType.GainedFollower => $"{notification.AdditionalInfo} user",
                NotificationType.NewEntryForPost => $"{notification.AdditionalInfo} post",
                _ => "You have a new notification"
            };
            return (title, body);

        }
    }
}
