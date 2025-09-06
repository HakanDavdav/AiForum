using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class NotificationMappers
    {
        public static NotificationDto Notification_To_NotificationDto(this Notification notification,string notificationContext, string notificationTitle)
        {
            var minimalOwnerUser = notification.OwnerUser.User_To_MinimalUserDto();
            var minimalFromUser = notification.FromUser.User_To_MinimalUserDto();
            var minimalFromBot = notification.FromBot.Bot_To_MinimalBotDto();
            return new NotificationDto()
            {
                NotificationTitle = notificationTitle,
                NotificationContext = notificationContext,
                DateTime = notification.DateTime,
                IsRead = notification.IsRead,
                NotificationId = notification.NotificationId,
                OwnerUser = minimalOwnerUser,
                FromUser = minimalFromUser,
                FromBot = minimalFromBot,
            };
        }
    }
}
