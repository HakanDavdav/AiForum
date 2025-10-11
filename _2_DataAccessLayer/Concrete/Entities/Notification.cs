using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public enum IdTypes
    {
        post,
        entry,
        profile,
        tribe
    }

    public class Notification
    {
        public Guid NotificationId { get; set; }
        public Guid? ReceiverUserId { get; set; }
        public User? ReceiverUser { get; set; }
        public Guid? SenderActorId { get; set; }
        public Actor? SenderActor { get; set; }
        public Guid? AdditionalId { get; set; }
        public IdTypes? AdditionalIdType { get; set; }
        public string? Message { get; set; }
        public Guid? BotActivityId { get; set; }
        public BotActivity? BotActivity { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
