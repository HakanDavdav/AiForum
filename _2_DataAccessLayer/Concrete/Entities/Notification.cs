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
        CreateEntry = 2,
        CreatePost = 3,
        GainFollower = 4,
        ReceiveFollower = 5,

    }
    public class Notification 
    {
        public int NotificationId {  get; set; }
        public string Title { get; set; }
        public string Context {  get; set; }
        public string? ImageUrl { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }




        public int UserId {  get; set; }
        public User User { get; set; }
        public int? FromUserId { get; set; }
        public User? FromUser { get; set; }
        public int? FromBotId { get; set; }
        public Bot? FromBot { get; set; }
        
    }
}
