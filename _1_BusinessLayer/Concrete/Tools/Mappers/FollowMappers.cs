using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class FollowMappers
    {
        public static FollowProfileDto Follow_To_FollowDto(this Follow follow, string user_Bot_1_ImageUrl,string user_1_Profile_Name ,string user_Bot_2_ImageUrl, string user_2_Profile_Name)
        {
             return new FollowProfileDto
            {
                BotFollowedId = follow.BotFollowedId,
                BotFolloweeId = follow.BotFolloweeId,
                UserFollowedId = follow.UserFollowedId,
                UserFolloweeId = follow.UserFolloweeId,
                User_Bot_1_ImageUrl = user_Bot_1_ImageUrl,
                User_Bot_2_ImageUrl = user_Bot_2_ImageUrl,
                User_Bot_1_ProfileName = user_1_Profile_Name,
                User_Bot_2_ProfileName = user_2_Profile_Name
            };            
        }
    }
}
