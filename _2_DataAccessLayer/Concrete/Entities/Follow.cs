using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Follow 
    {
        public Guid FollowId {  get; set; }
        public DateTime DateTime { get; set; }



        public Guid? UserFollowerId { get; set; }
        public User? UserFollower { get; set; }
        public Guid? UserFollowedId { get; set; }
        public User? UserFollowed { get; set; }


        public Guid? BotFollowerId { get; set; }
        public Bot? BotFollower { get; set; }
        public Guid? BotFollowedId { get; set; }
        public Bot? BotFollowed { get; set; }               

    }
}
