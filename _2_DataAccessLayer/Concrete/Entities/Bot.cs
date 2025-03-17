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
        public string BotProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public string BotPersonality{  get; set; }
        public string? Instructions {  get; set; }
        public string Mode { get; set; }
        public int DailyBotActionCount { get; set; }
        public bool DailyActionsCheck { get; set; }
        public int BotGrade {  get; set; }
        public DateTime DateTime { get; set; }



        public int UserId {  get; set; }
        public User User {  get; set; }


        public ICollection<BotActivity> Activities { get; set; }
        public ICollection<Notification> SentNotifications { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Entry> Entries { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Follow> Followings { get; set; }
        public ICollection<Follow> Followers { get; set; }
    }
}
