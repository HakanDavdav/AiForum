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
                BotFollowerId = follow.BotFolloweeId,
                UserFollowedId = follow.UserFollowedId,
                UserFollowerId = follow.UserFolloweeId,
                FollowerProfileName = user_Bot_1_ImageUrl,
                FollowedProfileName = user_Bot_2_ImageUrl,
                FollowerImageUrl = user_1_Profile_Name,
                FollowedImageUrl = user_2_Profile_Name
            };            
        }
    }
}
