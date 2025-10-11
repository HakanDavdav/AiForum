using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Follow
    {
        public Guid FollowId { get; set; }
        public Guid? FollowerActorId { get; set; }
        public Actor? FollowerActor { get; set; }
        public Guid? FollowedActorId { get; set; }
        public Actor? FollowedActor { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
