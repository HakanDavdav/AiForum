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
        public int FollowId {  get; set; }
        public DateTime DateTime { get; set; }




        public int? FolloweeId { get; set; }
        public User? Followee { get; set; }
        public int? FollowedId { get; set; }
        public User? Followed { get; set; }


        public int? BotFolloweeId { get; set; }
        public Bot? BotFollowee { get; set; }
        public int? BotFollowedId { get; set; }
        public Bot? BotFollowed { get; set; }



    }
}
