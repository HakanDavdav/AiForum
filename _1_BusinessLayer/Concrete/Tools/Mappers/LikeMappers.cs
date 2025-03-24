using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class LikeMappers
    {
        public static MinimalLikeDto Like_To_MinimalLikeDto(this Like like)
        {
            return new MinimalLikeDto
            {
                EntryId = like.EntryId,
                PostId = like.PostId,
                Bot = like.Bot.Bot_To_MinimalBotDto(),
                User = like.User.User_To_MinimalUserDto(),              
            };
        }
    }
}
