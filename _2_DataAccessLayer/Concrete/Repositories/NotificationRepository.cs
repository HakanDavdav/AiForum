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

        public override async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public override async Task DeleteAsync(Notification t)
        {
            try
            {
                _context.Notifications.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for NotificationId {NotificationId}", t.NotificationId);
                throw;
            }
        }


        public override async Task<Notification> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Notifications.FirstOrDefaultAsync(n => n.NotificationId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with NotificationId {NotificationId}", id);
                throw;
            }
        }

        public override async Task ManuallyInsertAsync(Notification t)
        {
            try
            {
                await _context.Notifications.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ManuallyInsertAsync for NotificationId {NotificationId}", t.NotificationId);
                throw;
            }
        }

        public override async Task UpdateAsync(Notification t)
        {
            try
            {
                _context.Notifications.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for NotificationId {NotificationId}", t.NotificationId);
                throw;
            }
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
                .Where(notification => notification.UserId == id)
                .OrderByDescending(notification => notification.DateTime)
                .Skip(startInterval)
                .Take(endInterval - startInterval)
                .Select(notification => new Notification
                {
                    NotificationId = notification.NotificationId,
                    NotificationContext = notification.NotificationContext,
                    DateTime = notification.DateTime,
                    IsRead = notification.IsRead,
                    UserId = notification.UserId,
                    User = notification.User,
                    ImageUrl = notification.ImageUrl,
                    Title = notification.Title,                    
                });
            return notifications.ToListAsync();
        }
    }
}
