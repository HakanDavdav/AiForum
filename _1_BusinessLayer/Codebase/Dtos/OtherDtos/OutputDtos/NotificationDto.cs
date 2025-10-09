using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.ActorDtos.OutputDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Dtos.OtherDtos.OutputDtos
{
    public class NotificationDto
    {
        public Guid? NotificationId { get; set; }
        public MinimalActorDto? MinimalSenderActor { get; set; }
        public Guid? AdditionalId { get; set; }
        public IdTypes? AdditionalIdType { get; set; }
        public string? Message { get; set; }
        public BotActivityDto? BotActivityDto { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
