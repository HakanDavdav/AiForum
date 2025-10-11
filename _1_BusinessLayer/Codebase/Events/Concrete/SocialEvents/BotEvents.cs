using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events.Interfaces;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Entities._2_DataAccessLayer.Concrete.Enums.OtherEnums;

namespace _1_BusinessLayer.Codebase.Events.Concrete.SocialEvents
{

    public class BotCreatedEvent : ISocialEvent
    {
        public Guid ActorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Bio { get; set; }
        public TopicTypes? Interests { get; set; }
        public bool? AutoInterests { get; set; }
        public bool? AutoBio { get; set; }
        public string? BotPersonality { get; set; }
        public string? Instructions { get; set; }
        public int? DailyBotOperationCount { get; set; }
        public BotModes? BotMode { get; set; }
    }

    public class ReplyCommandEvent : ISocialEvent
    {
        public Guid RepliedContentItemId { get; set; }
        public Guid SelectedBotId { get; set; }
        public Guid ActorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
