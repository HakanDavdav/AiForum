﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class FollowRepository : AbstractFollowRepository
    {
        public FollowRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<bool> CheckEntity(int id)
        {
            try
            {
                return await _context.Follows.AnyAsync(follow => follow.FollowId == id);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in CheckEntity: {sqlEx.Message}");
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in CheckEntity: {invalidOpEx.Message}");
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in CheckEntity: {dbUpdateEx.Message}");
                throw;
            }

        }
        public override async Task DeleteAsync(Follow t)
        {
            try
            {
                _context.Follows.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in DeleteAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in DeleteAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in DeleteAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task<List<Follow>> GetAllByBotIdAsync(int id)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.BotFolloweeId == id);
                return await follows.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetAllByBotIdAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetAllByBotIdAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetAllByBotIdAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task<List<Follow>> GetAllByUserIdAsync(int id)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.UserFolloweeId == id);
                return await follows.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetAllByUserIdAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetAllByUserIdAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetAllByUserIdAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task<Follow> GetByIdAsync(int id)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Follow follow = await _context.Follows.FirstOrDefaultAsync(follow => follow.FollowId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                return follow;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetByIdAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in GetByIdAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in GetByIdAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task InsertAsync(Follow t)
        {
            try
            {
                await _context.Follows.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in InsertAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in InsertAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in InsertAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }

        public override async Task UpdateAsync(Follow t)
        {
            try
            {
                _context.Follows.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in UpdateAsync: {sqlEx.Message}");
                throw; // Rethrow the exception
            }
            catch (InvalidOperationException invalidOpEx)
            {
                Console.WriteLine($"Invalid Operation Error in UpdateAsync: {invalidOpEx.Message}");
                throw; // Rethrow the exception
            }
            catch (DbUpdateException dbUpdateEx)
            {
                Console.WriteLine($"Database Update Error in UpdateAsync: {dbUpdateEx.Message}");
                throw; // Rethrow the exception
            }
        }
    }
}
