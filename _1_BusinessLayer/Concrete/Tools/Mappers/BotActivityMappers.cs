using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class BotActivityMappers
    {
        public static BotActivityDto BotActivity_To_BotActivityDto(this BotActivity botActivity)
        {
            var minimalBot = botActivity.OwnerBot.Bot_To_MinimalBotDto();
            return new BotActivityDto
            {
                ActivityContext = botActivity.ActivityContext,
                ActivityId = botActivity.ActivityId,
                ActivityType = botActivity.ActivityType,
                DateTime = botActivity.DateTime,
                Bot = minimalBot,
            };
        }

    }
}
