using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Extensions.Mappers
{
    public static class FollowMappers
    {
        public static FollowProfileDto Follow_To_FollowProfileDto(this Follow follow)
        {
            var minimalFollowedUser = follow.UserFollowed.User_To_MinimalUserDto();
            var minimalFollowedBot = follow.BotFollowed.Bot_To_MinimalBotDto();

            var minimalFollowerUser = follow.UserFollower.User_To_MinimalUserDto();
            var minimalFollowerBot = follow.BotFollower.Bot_To_MinimalBotDto();

            return new FollowProfileDto
            {
                FollowId = follow.FollowId,
                BotFollowed = minimalFollowedBot,
                UserFollowed = minimalFollowedUser,
                BotFollower = minimalFollowerBot,
                UserFollower = minimalFollowerUser,
            };
        }
    }
}
