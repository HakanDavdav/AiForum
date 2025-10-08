using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Dtos.OtherDtos
{
    public class NotificationDto
    {
        public Guid NotificationId { get; set; }
        public Actor? SenderActor { get; set; }
        public Guid AdditionalId { get; set; }
        public IdTypes AdditionalIdType { get; set; }
        public string? Message { get; set; }
        public BotActivity? BotActivity { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
