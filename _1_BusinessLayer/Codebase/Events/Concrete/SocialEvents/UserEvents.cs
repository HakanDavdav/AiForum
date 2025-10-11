using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events.Interfaces;
using _2_DataAccessLayer.Concrete.Entities;
using _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos;
using _2_DataAccessLayer.Concrete.Entities._2_DataAccessLayer.Concrete.Enums.OtherEnums;

namespace _1_BusinessLayer.Concrete.Events.Concrete.SocialEvents
{
    public class EntryCreatedEvent : ISocialEvent
    {
        public Guid ContentItemId { get; set; }
        public Guid ActorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PostCreatedEvent : ISocialEvent
    {
        public Guid CreatedPostId { get; set; }
        public Guid ActorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class LikedEvent : ISocialEvent
    {
        public Guid ActorId { get; set; }
        public Guid contentItemId { get; set; }
        public ReactionType ReactionType { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RepliedEvent : ISocialEvent
    {
        public Guid RepliedContentItemId { get; set; }
        public Guid CreatedEntryId { get; set; }
        public Guid ActorId { get; set; }
        public DateTime CreatedAt { get; set; }

    }


}
