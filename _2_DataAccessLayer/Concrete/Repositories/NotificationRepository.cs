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
            _context.notifications.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<Notification>> GetAllAsync()
        {
            IQueryable<Notification> notifications = _context.notifications;                                                         
            return await notifications.ToListAsync();
        }

        public override async Task<List<Notification>> GetAllWithInfoAsync()
        {
            IQueryable<Notification> userNotifications = _context.notifications
                                                                 .Include(notification => notification.User)
                                                                 .Include(notification => notification.FromUser)
                                                                 .Include(notification => notification.FromBot);
            return await userNotifications.ToListAsync();
        }

        public override async Task<List<Notification>> GetAllByUserIdAsync(int id)
        {
            IQueryable<Notification> userNotifications = _context.notifications.Where(notification => notification.UserId == id);
            return await userNotifications.ToListAsync();
        }

        public override async Task<List<Notification>> GetAllByUserIdWithInfoAsync(int id)
        {
            IQueryable<Notification> userNotifications = _context.notifications.Where(notification => notification.UserId == id)
                                                                              .Include(notification => notification.User)
                                                                              .Include(notification => notification.FromUser)
                                                                              .Include(notification => notification.FromBot);
            return await userNotifications.ToListAsync();
        }
      

        public override async Task<Notification> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var notification = await _context.notifications.FirstOrDefaultAsync(notification => notification.NotificationId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return notification;
        }

        public override async Task<Notification> GetByIdWithInfoAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

            var notification = await _context.notifications.Include(notification => notification.User)
                                                           .Include(notification => notification.FromUser)
                                                           .Include(notification => notification.FromBot)
                                                           .FirstOrDefaultAsync(notification => notification.NotificationId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return notification;
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
