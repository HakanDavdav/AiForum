using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.BodyBuilders;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Tools.Senders
{
    public class NotificationSender
    {
        private readonly NotificationActivityBodyBuilder _notificationActivityBodyBuilder;
        public NotificationSender(NotificationActivityBodyBuilder notificationActivityBodyBuilder)
        {
            _notificationActivityBodyBuilder = notificationActivityBodyBuilder;
        }
        public async Task<IdentityResult> SendSocialNotificationAsync(User? FromUser, Bot? FromBot, User toUser, NotificationType type, string additionalInfo, int additionalId)
        {
            var (title, notificationContext, url) = _notificationActivityBodyBuilder.BuildWebPushNotificationContent(FromUser, FromBot, type, additionalInfo, additionalId);
            //firebase
            return IdentityResult.Success;
        }


    }
}
