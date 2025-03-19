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
        public static NotificationDto Notification_To_NotificationDto(this Notification notification)
        {
            return new NotificationDto()
            {
                NotificationId = notification.NotificationId,
                Context = notification.Context,
                DateTime = notification.DateTime,
                FromBot = notification.FromBot,
                FromUser = notification.FromUser,
                ImageUrl = notification.ImageUrl,
                IsRead = notification.IsRead,
                Title = notification.Title,
            };
        }
    }
}
