using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Enums;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Follow 
    {
        public Guid FollowId {  get; set; }
        public DateTime DateTime { get; set; }



        public Guid? ActorFollowerId { get; set; }
        public Actor? ActorFollower { get; set; }

        public Guid? ActorFollowedId { get; set; }
        public Actor? ActorFollowed { get; set; }


    }
}
