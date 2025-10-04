using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events.Interfaces;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Events.Concrete.SocialEvents
{
    public class EntryCreated : ISocialEvent
    {
        public Guid CreatedEntryId { get; set; }
        public Guid CreatorActorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class BotCreated : ISocialEvent
    {
        public Guid CreatedBotId { get; set; }
        public Guid CreatorActorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PostCreated : ISocialEvent
    {
        public Guid CreatedPostId { get; set; }
        public Guid CreatorActorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class Liked : ISocialEvent
    {
        public Guid CreatorActorId { get; set; }
        public Guid LikedContentItemId { get; set; }
        public ReactionType ReactionType { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class Replied : ISocialEvent
    {
        public Guid RepliedContentItemId { get; set; }
        public Guid CreatedEntryId { get; set; }
        public Guid CreatorActorId { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    public class ReplyCommand : ISocialEvent
    {
        public Guid RepliedContentItemId { get; set; }
        public Guid SelectedBotId { get; set; }
        public Guid CreatorActorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
