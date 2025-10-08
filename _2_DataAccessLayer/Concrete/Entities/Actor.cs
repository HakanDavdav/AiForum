using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities._2_DataAccessLayer.Concrete.Enums.OtherEnums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace _2_DataAccessLayer.Concrete.Entities
{
    [Flags]
    public enum UserFeatures
    {
        ExtendedBotLimit,
        IncreasedOperationLimit,
    }
    [Flags]
    public enum BotCapabilities
    {
        AdvancedIntelligence,
        BotMemory,
    }
    public enum BotModes
    {
        Default,
        Opposing,
        Creative,
    }
    public enum BotGrades
    {
        A,
        B,
        C,
        D,
        F
    }
    public enum ThemeOptions
    {
        Light,
        Dark,
    }

    public abstract class Actor
    {
        public Guid ActorId { get; set; }
        public int ActorPoint { get; set; }
        public string? ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Bio { get; set; }
        public ICollection<TribeMembership>? TribeMemberships { get; set; }
        public ICollection<Bot>? Bots { get; set; }
        public TopicTypes Interests { get; set; }
        public int LikeCount { get; set; }
        public int EntryCount { get; set; }
        public int PostCount { get; set; }
        public int FollowerCount { get; set; }
        public int FollowedCount { get; set; }
        public DateTime CreatedAt { get; set; }

    }
    public class User : Actor
    {
        public UserIdentity? UserIdentity { get; set; }
        public UserSettings? UserSettings { get; set; }

    }

    public class UserSettings
    {
        // No need for user navigation property here, unnecessary direction
        public Guid ActorId { get; set; }
        public bool IsProfileCreated { get; set; }
        public UserFeatures PremiumFeatures { get; set; }
        public ThemeOptions Theme { get; set; }
        public int EntryPerPage { get; set; }
        public int PostPerPage { get; set; }
        public bool SocialNotificationPreference { get; set; }
        public bool SocialEmailPreference { get; set; }




    }
    public class UserIdentity : IdentityUser<Guid>
    {
    }

    public class UserRole : IdentityRole<Guid>
    {
    }



    public class Bot : Actor
    {
        public Guid ActorId { get; set; }
        public Actor? Actor { get; set; }
        public BotSettings? BotSettings { get; set; }

    }

    public class BotSettings
    {
        // No need for bot navigation property here, unnecessary direction
        public Guid ActorId { get; set; }
        public string? BotPersonality { get; set; }
        public string? Instructions { get; set; }
        public int DailyBotOperationCount { get; set; }
        public bool DailyOperationCheck { get; set; }
        public BotCapabilities BotCapabilities { get; set; }
        public BotModes BotMode { get; set; }
        public BotGrades BotGrade { get; set; }



    }

}
