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

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Notifications.AnyAsync(n => n.NotificationId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckEntity with NotificationId {NotificationId}", id);
                throw;
            }
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

        public override async Task<List<Notification>> GetAllByUserIdAsync(int id)
        {
            try
            {
                var query = _context.Notifications.Where(n => n.UserId == id);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllByUserIdWithIntervalsAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override Task<List<Notification>> GetAllWithCustomSearch(Func<IQueryable<Notification>, IQueryable<Notification>> queryModifier)
        {
            throw new NotImplementedException();
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

        public override async Task InsertAsync(Notification t)
        {
            try
            {
                await _context.Notifications.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InsertAsync for NotificationId {NotificationId}", t.NotificationId);
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
    }
}
