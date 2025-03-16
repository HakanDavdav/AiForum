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
    public class NewsRepository : AbstractNewsRepository
    {
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Method to delete a news item asynchronously
        public override async Task DeleteAsync(News t)
        {
            try
            {
                _context.Remove(t); // Remove the news item from context
                await _context.SaveChangesAsync(); // Save changes to the database
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

        // Method to get a news item by its ID asynchronously
        public override async Task<News> GetByIdAsync(int id)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                News news = await _context.News.FirstOrDefaultAsync(news => news.NewsId == id); // Fetch the news item
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                return news; // Return the found news item
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

        // Method to get a random set of news items
        public override async Task<List<News>> GetRandomNews(int number)
        {
            try
            {
                // Randomize the order and select a specified number of news items
                IQueryable<News> news = _context.News.OrderBy(news => Guid.NewGuid()).Take(number);
                return await news.ToListAsync(); // Return the list of random news items
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

        // Method to insert a new news item asynchronously
        public override async Task InsertAsync(News t)
        {
            try
            {
                await _context.News.AddAsync(t); // Add the news item to the context
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

        // UpdateAsync method is not implemented yet
        public override Task UpdateAsync(News t)
        {
            throw new NotImplementedException(); // Not implemented exception
        }
    }
}
