using System;
using _2_DataAccessLayer.Concrete.Entities;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Tools.AuthIntegrations.BodyBuilders
{
    public class NotificationActivityBodyBuilder
    {
        /// <summary>
        /// Bot aktiviteleri için title ve body oluşturur.
        /// </summary>
        public (string title, string body) BuildBotActivityContent(
            Bot bot,
            BotActivityType type,
            string additionalInfo)
        {
            string botName = bot?.BotProfileName ?? "A bot";

            string title = type switch
            {
                BotActivityType.CreatingEntry => $"{botName} created a new entry",
                BotActivityType.CreatingPost => $"{botName} created a new post",
                BotActivityType.EntryLike => $"{botName} liked an entry",
                BotActivityType.PostLike => $"{botName} liked a post",
                BotActivityType.StartingFollow => $"{botName} started following someone",
                BotActivityType.GainedFollower => $"{botName} gained a new follower",
                _ => "Bot Activity"
            };

            string body = type switch
            {
                BotActivityType.CreatingEntry => $"A new {additionalInfo} entry has been created by {botName}.",
                BotActivityType.CreatingPost => $"A new {additionalInfo} post has been created by {botName}.",
                BotActivityType.EntryLike => $"{botName} liked {additionalInfo} entry.",
                BotActivityType.PostLike => $"{botName} liked {additionalInfo} post.",
                BotActivityType.StartingFollow => $"{botName} started following {additionalInfo}.",
                BotActivityType.GainedFollower => $"{botName} gained a new follower: {additionalInfo}.",
                _ => "You have a new bot activity."
            };

            return (title, body);
        }


        public (string title, string body, string url) BuildNotificationContent(
            User? FromUser,
            Bot? FromBot,
            NotificationType type,
            string additionalInfo,
            int additionalId)
        {
            string senderName = FromUser?.UserName ?? FromBot?.BotProfileName ?? "Someone";

            string title = type switch
            {
                NotificationType.EntryLike => $"{senderName} liked your entry",
                NotificationType.PostLike => $"{senderName} liked your post",
                NotificationType.CreatingEntry => $"{senderName} created a new entry",
                NotificationType.CreatingPost => $"{senderName} created a new post",
                NotificationType.GainedFollower => $"{senderName} started following you",
                NotificationType.NewEntryForPost => $"{senderName} added a new entry to your post",
                _ => "Notification"
            };

            string body = type switch
            {
                NotificationType.EntryLike => $"{additionalInfo} entry",
                NotificationType.PostLike => $"{additionalInfo} post",
                NotificationType.CreatingEntry => $"{additionalInfo} entry",
                NotificationType.CreatingPost => $"{additionalInfo} post",
                NotificationType.GainedFollower => $"{senderName} user",
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
    }
}
