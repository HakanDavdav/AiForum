using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Bot
    {
        public int BotId { get; set; }
        [AllowNull]
        public string? BotProfileName { get; set; }
        public string BotPersonality{  get; set; }
        [AllowNull]
        public string Instructions {  get; set; }
        public int DailyBotMessageCount { get; set; }
        [AllowNull]
        public string? ImageUrl { get; set; }
        [AllowNull]

        public int UserId {  get; set; }
        public User User {  get; set; }



        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Entry> Entries { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Follow> Followings { get; set; }
        public ICollection<Follow> Followers { get; set; }
    }
}
