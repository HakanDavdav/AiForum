using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Factories
{
    public class NotificationEventFactory
    {
        public List<NotificationEvent> CreateNotificationEvents(User fromUser,Bot fromBot,List<int> toUserId,NotificationType type,string additionalInfo,int additionalId, int startInterval, int endInterval)
        {
            List<NotificationEvent> notificationEvents = new List<NotificationEvent>();
            for (int i = startInterval; i < endInterval; i++)
            {
                notificationEvents.Add(new NotificationEvent
                {
                    AdditionalInfo = additionalInfo,
                    AdditionalId = additionalId,
                    Type = type,
                    ReceiverUserId = toUserId[i],
                    CreatedAt = DateTime.Now,
                    SenderBotId = fromBot.BotId,
                    SenderUserId = fromUser.Id,
                });
            }
            return notificationEvents;
        }
    }
}
