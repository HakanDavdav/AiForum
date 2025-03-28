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
    public class FollowRepository : AbstractFollowRepository
    {
        public FollowRepository(ApplicationDbContext context, ILogger<Follow> logger) : base(context, logger)
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
                _logger.LogError(sqlEx, "SQL Error in CheckEntity with FollowId {FollowId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in CheckEntity with FollowId {FollowId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in CheckEntity with FollowId {FollowId}", id);
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
                _logger.LogError(sqlEx, "SQL Error in DeleteAsync for FollowId {FollowId}", t.FollowId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in DeleteAsync for FollowId {FollowId}", t.FollowId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in DeleteAsync for FollowId {FollowId}", t.FollowId);
                throw;
            }
        }

        public override async Task<List<Follow>> GetAllByUserIdAsFollowerWithInfoAsync(int id)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.UserFollowerId == id)
                                              .Include(follow => follow.UserFollower)
                                              .Include(follow => follow.BotFollower)
                                              .Include(follow => follow.UserFollowed)
                                              .Include(follow => follow.BotFollowed);
                return await follows.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetAllByUserIdAsFollowerWithInfoAsync with UserId {UserId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetAllByUserIdAsFollowerWithInfoAsync with UserId {UserId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetAllByUserIdAsFollowerWithInfoAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task<List<Follow>> GetAllByUserIdAsFollowedWithInfoAsync(int id)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.UserFollowedId == id)
                                              .Include(follow => follow.UserFollower)
                                              .Include(follow => follow.BotFollower)
                                              .Include(follow => follow.UserFollowed)
                                              .Include(follow => follow.BotFollowed);
                return await follows.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetAllByUserIdAsFollowedWithInfoAsync with UserId {UserId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetAllByUserIdAsFollowedWithInfoAsync with UserId {UserId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetAllByUserIdAsFollowedWithInfoAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task<List<Follow>> GetAllByBotIdAsFollowerWithInfoAsync(int id)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.BotFollowerId == id)
                                              .Include(follow => follow.UserFollower)
                                              .Include(follow => follow.BotFollower)
                                              .Include(follow => follow.UserFollowed)
                                              .Include(follow => follow.BotFollowed);
                return await follows.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetAllByBotIdAsFollowerWithInfoAsync with BotId {BotId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetAllByBotIdAsFollowerWithInfoAsync with BotId {BotId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetAllByBotIdAsFollowerWithInfoAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Follow>> GetAllByBotIdAsFollowedWithInfoAsync(int id)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.BotFollowedId == id)
                                              .Include(follow => follow.UserFollower)
                                              .Include(follow => follow.BotFollower)
                                              .Include(follow => follow.UserFollowed)
                                              .Include(follow => follow.BotFollowed);
                return await follows.ToListAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetAllByBotIdAsFollowedWithInfoAsync with BotId {BotId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetAllByBotIdAsFollowedWithInfoAsync with BotId {BotId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetAllByBotIdAsFollowedWithInfoAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override Task<List<Follow>> GetAllWithCustomSearch(Func<IQueryable<Follow>, IQueryable<Follow>> queryModifier)
        {
            throw new NotImplementedException();
        }

        public override async Task<Follow> GetByIdAsync(int id)
        {
            try
            {
                Follow follow = await _context.Follows.FirstOrDefaultAsync(follow => follow.FollowId == id);
                return follow;
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL Error in GetByIdAsync with FollowId {FollowId}", id);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in GetByIdAsync with FollowId {FollowId}", id);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in GetByIdAsync with FollowId {FollowId}", id);
                throw;
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
                _logger.LogError(sqlEx, "SQL Error in InsertAsync for FollowId {FollowId}", t.FollowId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in InsertAsync for FollowId {FollowId}", t.FollowId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in InsertAsync for FollowId {FollowId}", t.FollowId);
                throw;
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
                _logger.LogError(sqlEx, "SQL Error in UpdateAsync for FollowId {FollowId}", t.FollowId);
                throw;
            }
            catch (InvalidOperationException invalidOpEx)
            {
                _logger.LogError(invalidOpEx, "Invalid Operation Error in UpdateAsync for FollowId {FollowId}", t.FollowId);
                throw;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                _logger.LogError(dbUpdateEx, "Database Update Error in UpdateAsync for FollowId {FollowId}", t.FollowId);
                throw;
            }
        }
    }
}
