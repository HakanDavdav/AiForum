using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class NotificationRepository : AbstractNotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context, ILogger<Notification> logger) : base(context, logger)
        {
        }


        public override async Task<List<Notification>> GetWithCustomSearchAsync(Func<IQueryable<Notification>, IQueryable<Notification>> queryModifier)
        {
            IQueryable<Notification> query = _context.Notifications;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }

        public override async Task<Notification> GetBySpecificPropertySingularAsync(Func<IQueryable<Notification>, IQueryable<Notification>> queryModifier)
        {
            IQueryable<Notification> query = _context.Notifications;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }


        public override Task<List<Notification>> GetNotificationModulesForUser(int id, int startInterval, int endInterval)
        {
            var notifications = _context.Notifications
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

    }
}
