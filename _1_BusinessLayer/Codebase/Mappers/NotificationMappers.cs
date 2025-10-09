using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.OtherDtos.OutputDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Mappers
{
    public static class NotificationMappers
    {
        public static NotificationDto Notification_To_NotificationDto(this Notification? notification)
        {
            return new NotificationDto
            {
                CreatedAt = notification?.CreatedAt,
                IsRead = notification?.IsRead,
                Message = notification?.Message,
                AdditionalId = notification?.AdditionalId,
                AdditionalIdType = notification?.AdditionalIdType,
                MinimalSenderActor = notification?.SenderActor.Actor_To_MinimalActorDto(),
                BotActivityDto = notification?.BotActivity.BotActivity_To_BotActivityDto()
            };
        }
    }
}
