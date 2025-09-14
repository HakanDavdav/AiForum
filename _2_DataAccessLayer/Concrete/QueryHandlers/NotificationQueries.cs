using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Abstractions.Interfaces;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.Queries
{
    public class NotificationQueries : AbstractNotificationQueryHandler
    {
        public NotificationQueries(ILogger<Notification> logger, AbstractGenericBaseCommandHandler repository) : base(logger, repository)
        {
        }

        public override Task<List<Notification>> GetNotificationModulesForUserAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                var notifications = _repository.Export<Notification>()
            .Where(notification => notification.OwnerUserId == id)
            .OrderByDescending(notification => notification.DateTime)
            .Skip(startInterval)
            .Take(endInterval - startInterval)
            .Select(notification => new Notification
            {
                NotificationId = notification.NotificationId,
                DateTime = notification.DateTime,
                IsRead = notification.IsRead,
                FromBot = notification.FromBot,
                FromUser = notification.FromUser,
                OwnerUser = notification.OwnerUser,
                AdditionalId = notification.AdditionalId,
                NotificationType = notification.NotificationType,
            });
                return notifications.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

    }
}
