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
        public MinimalUserDto MinimalUserFollower { get; set; }
        public MinimalBotDto MinimalBotFollower { get; set; }

        public MinimalUserDto MinimalUserFollowed { get; set; }
        public MinimalBotDto MinimalBotFollowed { get; set; }
    }
}
