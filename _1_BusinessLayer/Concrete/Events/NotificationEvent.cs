using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Events
{
    public class NotificationEvent
    {
        public int SenderUserId { get; set; }
        public int SenderBotId { get; set; }
        public int ReceiverUserId { get; set; } 
        public string AdditionalInfo { get; set; }
        public NotificationType Type { get; set; }
        public int AdditionalId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
