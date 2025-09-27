using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;


namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Notification 
    {
        public Guid NotificationId {  get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType NotificationType { get; set; }


        public BotActivity? BotActivityJson { get; set; }
        public Guid? FromUserId { get; set; }
        public User? FromUser { get; set; }
        public Guid? FromBotId { get; set; }
        public Bot? FromBot { get; set; }
        public Guid? AdditionalId { get; set; }
        public string? AdditionalInfo { get; set; }




        public Guid? OwnerUserId {  get; set; } 
        public User? OwnerUser { get; set; } 

    }
}
