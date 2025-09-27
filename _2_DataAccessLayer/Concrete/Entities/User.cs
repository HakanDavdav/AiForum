using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public string? City { get; set; }
        public string? Bio { get; set; }
        public bool IsProfileCreated { get; set; }
        public DateTime DateTime { get; set; } 
        public PremiumFeatures PremiumFeatures { get; set; }
        public TopicTypes Interests { get; set; }
        public int LikeCount { get; set; }
        public int EntryCount { get; set; }
        public int PostCount { get; set; }
        public int FollowerCount { get; set; }
        public int FollowedCount { get; set; }




        public UserPreference UserPreference { get; set; }
        public ICollection<Notification> ReceivedNotifications { get; set; }
        public ICollection<Notification> SentNotifications { get; set; }
        public ICollection<Bot> Bots { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Entry> Entries { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Follow> Followed { get; set; }
        public ICollection<Follow> Followers { get; set; }

        
    }
}
