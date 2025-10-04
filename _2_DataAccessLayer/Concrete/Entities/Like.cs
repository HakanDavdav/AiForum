using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public enum ReactionType
    {
        Like = 0,
        Dislike = 1,
        BrutallyDislike = 2,
    }
    public class Like
    {
        public Guid LikeId { get; set; }
        public Guid ActorId { get; set; }
        public Actor? Actor { get; set; }
        public Guid ContentItemId { get; set; }
        public ContentItem? ContentItem { get; set; }
        public ReactionType ReactionType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
