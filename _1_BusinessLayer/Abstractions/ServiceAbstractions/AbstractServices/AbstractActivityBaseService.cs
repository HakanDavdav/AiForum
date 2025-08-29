using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractActivityBaseService : IActivityBaseService
    {
        protected readonly AbstractActivityRepository _activityRepository;
        protected readonly AbstractNotificationRepository _notificationRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractBotRepository _botRepository;
        public AbstractActivityBaseService(AbstractActivityRepository activityRepository,AbstractNotificationRepository notificationRepository, AbstractUserRepository userRepository,AbstractBotRepository botRepository)
        {
            _activityRepository = activityRepository;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _botRepository = botRepository;

        }

        public abstract Task<IdentityResult> ClearNotifications(int userId);
        public abstract Task<IdentityResult> CreateBotActivityAsync(int botId, ActivityType type, string additionalInfo, int additionalId);
        public abstract Task<IdentityResult> CreateNotificationAsync(User FromUser, Bot FromBot, List<User> ToUsers, NotificationType type, int additionalInfo, int additionalId);
        public abstract Task<IdentityResult> MarkAsRead(int userId, int[] notificationIds);
    }
}
