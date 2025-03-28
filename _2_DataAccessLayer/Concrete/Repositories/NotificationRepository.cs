using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                return await _context.Notifications.AnyAsync(notification => notification.NotificationId == id);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in CheckEntity with NotificationId {NotificationId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in CheckEntity with NotificationId {NotificationId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in CheckEntity with NotificationId {NotificationId}", id);
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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in DeleteAsync for NotificationId {NotificationId}", t.NotificationId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in DeleteAsync for NotificationId {NotificationId}", t.NotificationId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in DeleteAsync for NotificationId {NotificationId}", t.NotificationId);
                throw;
            }
        }

        public override async Task<List<Notification>> GetAllByUserIdAsync(int id)
        {
            try
            {
                IQueryable<Notification> notifications = _context.Notifications.Where(notification => notification.UserId == id);
                return await notifications.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetAllByUserIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetAllByUserIdAsync with UserId {UserId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetAllByUserIdAsync with UserId {UserId}", id);
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
                var notification = await _context.Notifications.FirstOrDefaultAsync(notification => notification.NotificationId == id);
                return notification;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByIdAsync with NotificationId {NotificationId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByIdAsync with NotificationId {NotificationId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByIdAsync with NotificationId {NotificationId}", id);
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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in InsertAsync for NotificationId {NotificationId}", t.NotificationId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in InsertAsync for NotificationId {NotificationId}", t.NotificationId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in InsertAsync for NotificationId {NotificationId}", t.NotificationId);
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
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in UpdateAsync for NotificationId {NotificationId}", t.NotificationId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in UpdateAsync for NotificationId {NotificationId}", t.NotificationId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in UpdateAsync for NotificationId {NotificationId}", t.NotificationId);
                throw;
            }
        }
    }
}
