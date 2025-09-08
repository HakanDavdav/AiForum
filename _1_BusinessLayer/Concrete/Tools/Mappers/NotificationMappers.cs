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
            return new NotificationDto()
            {
                NotificationTitle = notificationTitle,
                NotificationContext = notificationContext,
                DateTime = notification.DateTime,
                IsRead = notification.IsRead,
                NotificationId = notification.NotificationId,
                AdditionalId = notification.AdditionalId,
                NotificationType = notification.NotificationType,

            };
        }
    }
}
