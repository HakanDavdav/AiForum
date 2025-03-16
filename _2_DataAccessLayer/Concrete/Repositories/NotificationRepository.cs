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

        // Method to delete a notification asynchronously
        public override async Task DeleteAsync(Notification t)
        {
            try
            {
                _context.Notifications.Remove(t); // Remove the notification from context
                await _context.SaveChangesAsync(); // Save changes to database
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL-related errors (Connection error, timeout, syntax error, etc.)
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Invalid operation error (Context closed, object tracking issue, etc.)
                Console.WriteLine($"Invalid Operation Error: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Database update error (Foreign Key violation, Unique Key violation, etc.)
                Console.WriteLine($"Database Update Error: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        // Method to get all notifications by User ID asynchronously
        public override async Task<List<Notification>> GetAllByUserIdAsync(int id)
        {
            try
            {
                IQueryable<Notification> notifications = _context.Notifications.Where(notification => notification.UserId == id);
                return await notifications.ToListAsync(); // Get notifications as a list
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL-related errors
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Invalid operation error
                Console.WriteLine($"Invalid Operation Error: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Database update error
                Console.WriteLine($"Database Update Error: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        // Method to get notification by its ID asynchronously
        public override async Task<Notification> GetByIdAsync(int id)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                var notification = await _context.Notifications.FirstOrDefaultAsync(notification => notification.NotificationId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                return notification; // Return the found notification
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL-related errors
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Invalid operation error
                Console.WriteLine($"Invalid Operation Error: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Database update error
                Console.WriteLine($"Database Update Error: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        // Method to insert a new notification asynchronously
        public override async Task InsertAsync(Notification t)
        {
            try
            {
                await _context.Notifications.AddAsync(t); // Add the new notification to the context
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL-related errors
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Invalid operation error
                Console.WriteLine($"Invalid Operation Error: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Database update error
                Console.WriteLine($"Database Update Error: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        // Method to update an existing notification asynchronously
        public override async Task UpdateAsync(Notification t)
        {
            try
            {
                _context.Update(t); // Update the notification in the context
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // SQL-related errors
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Invalid operation error
                Console.WriteLine($"Invalid Operation Error: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // Database update error
                Console.WriteLine($"Database Update Error: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }
    }
}
