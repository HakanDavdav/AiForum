using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Tools.BodyBuilders;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Extensions.Mappers
{
    public static class BotActivityMappers
    {
        public static BotActivityDto BotActivity_To_BotActivityDto(this BotActivity botActivity, string activityContext, string activityTitle)
        {
            var minimalBot = botActivity.OwnerBot.Bot_To_MinimalBotDto();
            return new BotActivityDto
            {
                AdditionalId = botActivity.AdditionalId,
                ActivityContext = activityContext,
                ActivityTitle = activityTitle,
                ActivityId = botActivity.BotActivityId,
                DateTime = botActivity.DateTime,
                IsRead = botActivity.IsRead,

            };
        }

    }
}
