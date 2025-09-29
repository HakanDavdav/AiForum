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
        public int FollowId {  get; set; }
        public DateTime DateTime { get; set; }



        public int? UserFollowerId { get; set; }
        public User? UserFollower { get; set; }
        public int? UserFollowedId { get; set; }
        public User? UserFollowed { get; set; }


        public int? BotFollowerId { get; set; }
        public Bot? BotFollower { get; set; }
        public int? BotFollowedId { get; set; }
        public Bot? BotFollowed { get; set; }               

    }
}
