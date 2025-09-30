using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Enums.BotEnums;
using _2_DataAccessLayer.Concrete.Enums.OtherEnums;
using Microsoft.AspNetCore.Identity;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public abstract class Actor
    {
        public Guid ActorId { get; set; }
        public string? ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Bio { get; set; }
        public DateTime DateTime { get; set; }
        public int LikeCount { get; set; }
        public int EntryCount { get; set; }
        public int PostCount { get; set; }
        public int FollowerCount { get; set; }
        public int FollowedCount { get; set; }
        public TopicTypes Interests { get; set; }
        public ICollection<Notification> SentNotifications { get; set; }
        public ICollection<Bot> Bots { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Entry> Entries { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Follow> Followed { get; set; }
        public ICollection<Follow> Followers { get; set; }

    }
    public class User : Actor
    {
        public UserIdentity UserIdentity { get; set; }
        public UserSettings UserSettings { get; set; }
        public ICollection<Notification> ReceivedNotifications { get; set; }

    }

    public class UserSettings
    {
        public Guid ActorId { get; set; }
        public bool IsProfileCreated { get; set; }
        public PremiumFeatures PremiumFeatures { get; set; }
        public ThemeOptions Theme { get; set; }
        public int EntryPerPage { get; set; }
        public int PostPerPage { get; set; }
        public bool SocialNotificationPreference { get; set; }
        public bool SocialEmailPreference { get; set; }



    }
    public class UserIdentity : IdentityUser<Guid>
    {
        //public Guid ActorId { get; set; } => Id
    }

    public class UserRole : IdentityRole<Guid>
    {

    }



    public class Bot : Actor
    {
        public Guid OwnerActorId { get; set; }
        public Actor OwnerActor { get; set; }
        public BotSettings BotSettings { get; set; }
        public ICollection<BotMemoryLog> BotMemoryLogs { get; set; }
        public ICollection<BotActivity> BotActivities { get; set; }

    }

    public class BotSettings
    {
        public Guid ActorId { get; set; } 
        public string BotPersonality { get; set; }
        public string? Instructions { get; set; }
        public int DailyBotOperationCount { get; set; }
        public bool DailyOperationCheck { get; set; }
        public BotCapabilities BotCapabilities { get; set; }
        public BotModes BotMode { get; set; }
        public BotGrades BotGrade { get; set; }




    }

    public enum ThemeOptions
    {
        Light,
        Dark,
    }




}
