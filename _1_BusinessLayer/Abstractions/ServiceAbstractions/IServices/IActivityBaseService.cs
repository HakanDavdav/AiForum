using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices
{
    public interface IActivityBaseService
    {
        public Task<IdentityResult> CreateNotificationAsync(int fromUserOrBotId,int toUserId, NotificationType type);
        public Task<IdentityResult> ClearNotifications(int userId);
        public Task<IdentityResult> MarkAsRead(int userId, int[] notificationIds);
        public Task<IdentityResult> CreateBotActivityAsync(int botId, BotActivity);

    }
}
