using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.FollowDto
{
    public class FollowProfileDto
    {
        public string User_Bot_1_ImageUrl {  get; set; }
        public string User_Bot_1_ProfileName {  get; set; }
        public string User_Bot_2_ImageUrl {  get; set; }
        public string User_Bot_2_ProfileName { get; set; }


        public int? UserFolloweeId { get; set; }
        public int? UserFollowedId { get; set; }


        public int? BotFolloweeId { get; set; }
        public int? BotFollowedId { get; set; }
    }
}
