using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public enum NotificationType
    {
        Like = 1,
        CreatingEntry = 2,
        CreatingPost = 3,
        Message = 4,
        BotActivity = 5,
        FollowGain = 6,
    }
    public class Notification 
    {
        public int NotificationId {  get; set; }
        public string Title { get; set; }
        public string NotificationContext {  get; set; }
        public string? ImageUrl { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType NotificationType { get; set; }


        public int? FromUserId { get; set; }
        public User? FromUser { get; set; }
        public int? FromBotId { get; set; }
        public Bot? FromBot { get; set; }
        public int? AdditionalId { get; set; }



        public int UserId {  get; set; }
        public User User { get; set; }
        
    }
}
