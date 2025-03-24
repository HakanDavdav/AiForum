using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.FollowDto
{
    public class FollowProfileDto
    {
        public MinimalUserDto? UserFollower { get; set; }
        public MinimalBotDto? BotFollower { get; set; }

        public MinimalUserDto? UserFollowed { get; set; }
        public MinimalBotDto? BotFollowed { get; set; }
    }
}
