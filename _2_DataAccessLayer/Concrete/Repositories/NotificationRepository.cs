﻿using System;
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
            _context.Notifications.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<IQueryable<Notification>> GetAllAsync()
        {
            IQueryable<Notification> notifications = _context.Notifications;
            return notifications;
        }

        public override async Task<IQueryable<Notification>> GetAllWithInfoAsync()
        {
            IQueryable<Notification> userNotifications = _context.Notifications
                                                                 .Include(notification => notification.User)
                                                                 .Include(notification => notification.FromUser)
                                                                 .Include(notification => notification.FromBot);
            return userNotifications;
        }

        public override async Task<List<Notification>> GetAllByUserIdAsync(int id)
        {
            IQueryable<Notification> userNotifications = _context.Notifications.Where(notification => notification.UserId == id);
            return await userNotifications.ToListAsync();
        }

        public override async Task<List<Notification>> GetAllByUserIdWithInfoAsync(int id)
        {
            IQueryable<Notification> userNotifications = _context.Notifications.Where(notification => notification.UserId == id)
                                                                              .Include(notification => notification.User)
                                                                              .Include(notification => notification.FromUser)
                                                                              .Include(notification => notification.FromBot);
            return await userNotifications.ToListAsync();
        }
      

        public override async Task<Notification> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var notification = await _context.Notifications.FirstOrDefaultAsync(notification => notification.NotificationId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return notification;
        }

        public override async Task<Notification> GetByIdWithInfoAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

            var notification = await _context.Notifications.Include(notification => notification.User)
                                                           .Include(notification => notification.FromUser)
                                                           .Include(notification => notification.FromBot)
                                                           .FirstOrDefaultAsync(notification => notification.NotificationId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return notification;
        }

        public override async Task InsertAsync(Notification t)
        {
            await _context.Notifications.AddAsync(t);
            await _context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(Notification t)
        {
            _context.Update(t);
            await _context.SaveChangesAsync();

        }
    }
}
