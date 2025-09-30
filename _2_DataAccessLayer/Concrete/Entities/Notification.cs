using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Enums.OtherEnums;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;


namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Notification 
    {
        public int NotificationId {  get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType NotificationType { get; set; }


        public Guid? FromActorId { get; set; }
        public Actor? FromActor { get; set; }
        public int? AdditionalId { get; set; }
        public string? AdditionalInfo { get; set; }


        public int? ActorUserOwnerId {  get; set; } 
        public User? ActorUserOwner { get; set; } 

    }
}
