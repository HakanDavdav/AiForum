using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Events.Concrete.SocialEvents
{
    public enum PromotionType
    {
        Upvote = 0,
        Downvote = 1,
    }
    public class PromotionChangeEvent
    {
        public Guid ActorId { get; set; }
        public Guid TribeId { get; set; }
        public PromotionType PromotionType { get; set; }
        public DateTime PromotedAt { get; set; }
    }

    public class JoinedTribeEvent
    {
        public Guid ActorId { get; set; }
        public Guid TribeId { get; set; }
        public DateTime JoinedAt { get; set; }
    }

    public class LeftTribeEvent
    {
        public Guid ActorId { get; set; }
        public Guid TribeId { get; set; }
        public DateTime LeftAt { get; set; }
    }





}
