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
        public int NotificationId {  get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType NotificationType { get; set; }


        public int? FromUserId { get; set; }
        public User? FromUser { get; set; }
        public int? FromBotId { get; set; }
        public Bot? FromBot { get; set; }
        public int? AdditionalId { get; set; }
        public string? AdditionalInfo { get; set; }



        public int? OwnerUserId {  get; set; } 
        public User? OwnerUser { get; set; } 

    }
}
