using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class NotificationRepository : AbstractNotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(Notification t)
        {
            _context.notifications.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<Notification>> GetAllAsync()
        {
            IQueryable<Notification> notifications = _context.notifications
                                                             .Include(notification => notification.User)
                                                             .Include(notification => notification.FromUser)
                                                             .Include(notification => notification.FromBot);
            return await notifications.ToListAsync();
        }

        public override async Task<List<Notification>> GetAllByUserId(int id)
        {
            IQueryable<Notification> userNotifications = _context.notifications
                                                                 .Where(notification => notification.UserId == id)
                                                                 .Include(notification => notification.User)
                                                                 .Include(notification => notification.FromUser)
                                                                 .Include(notification => notification.FromBot);
            return await userNotifications.ToListAsync();
        }

        public override async Task<Notification> GetByIdAsync(int id)
        {
            var notification = await _context.notifications
                                             .Include(notification => notification.User)
                                             .Include(notification => notification.FromUser)
                                             .Include(notification => notification.FromBot)
                                             .FirstOrDefaultAsync(notification => notification.NotificationId == id);
#pragma warning disable CS8603 // Possible null reference return.
            return notification;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task InsertAsync(Notification t)
        {
            await _context.notifications.AddAsync(t);
            await _context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(Notification t)
        {
            _context.Attach(t);
            await _context.SaveChangesAsync();

        }
    }
}
