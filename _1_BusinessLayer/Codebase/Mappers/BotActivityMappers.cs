using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.OtherDtos.OutputDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Mappers
{
    public static class BotActivityMappers
    {
        public static BotActivityDto BotActivity_To_BotActivityDto(this BotActivity? botActivity)
        {
            return new BotActivityDto
            {
                AdditionalId = botActivity?.AdditionalId,
                AdditionalIdType = botActivity?.AdditionalIdType,
                CreatedAt = botActivity?.CreatedAt,
                MinimalActor = botActivity?.Bot.Actor_To_MinimalActorDto(),
                IsRead = botActivity?.IsRead,
                Message = botActivity?.Message,
            };
        }
    }
}
