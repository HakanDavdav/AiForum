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

    public class Notifications
    {
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid? SenderActorId { get; set; }
        public Actor? SenderActor { get; set; }
        public Guid AdditionalId { get; set; }
        public IdTypes AdditionalIdType { get; set; }
        public string? Message { get; set; }
        public Guid? BotActivityId { get; set; }
        public BotActivity? BotActivity { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
