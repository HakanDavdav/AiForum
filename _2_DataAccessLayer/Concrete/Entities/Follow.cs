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

        public Guid? FollowerId { get; set; }
        public ActorTypes FollowerType { get; set; }
        public User? UserFollower { get; set; }
        public Bot? BotFollower { get; set; }


        public Guid? FollowedId { get; set; }
        public ActorTypes FollowedType { get; set; }
        public Bot? BotFollowed { get; set; }
        public User? UserFollowed { get; set; }


    }
}
