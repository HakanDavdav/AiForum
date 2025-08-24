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

        public override async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public override async Task DeleteAsync(Follow t)
        {
            try
            {
                _context.Follows.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for FollowId {FollowId}", t.FollowId);
                throw;
            }
        }

        public override async Task<Follow> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Follows.FirstOrDefaultAsync(f => f.FollowId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with FollowId {FollowId}", id);
                throw;
            }
        }

        public override async Task ManuallyInsertAsync(Follow t)
        {
            try
            {
                await _context.Follows.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ManuallyInsertAsync for FollowId {FollowId}", t.FollowId);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for FollowId {FollowId}", t.FollowId);
                throw;
            }
        }

        public override async Task<List<Follow>> GetWithCustomSearchAsync(Func<IQueryable<Follow>, IQueryable<Follow>> queryModifier)
        {
            IQueryable<Follow> query = _context.Follows;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }

        public override async Task<Follow> GetBySpecificPropertySingularAsync(Func<IQueryable<Follow>, IQueryable<Follow>> queryModifier)
        {
            IQueryable<Follow> query = _context.Follows;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task<List<Follow>> GetFollowModulesForUserAsFollowerAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.UserFollowerId == id)
                                             .Skip(startInterval)
                                             .Take(endInterval - startInterval)
                                             .Select(follow => new Follow
                                             {
                                                 FollowId = follow.FollowId,
                                                 UserFollowerId = follow.UserFollowerId,
                                                 BotFollowerId = follow.BotFollowerId,
                                                 UserFollowedId = follow.UserFollowedId,
                                                 BotFollowedId = follow.BotFollowedId,
                                                 UserFollower = follow.UserFollower,
                                                 BotFollower = follow.BotFollower,
                                                 UserFollowed = follow.UserFollowed,
                                                 BotFollowed = follow.BotFollowed
                                             });

                return await follows.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFollowModulesForUserAsFollowerAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task<List<Follow>> GetFollowModulesForUserAsFollowedAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.UserFollowedId == id)
                             .Skip(startInterval)
                             .Take(endInterval - startInterval)
                             .Select(follow => new Follow
                             {
                                 FollowId = follow.FollowId,
                                 UserFollowerId = follow.UserFollowerId,
                                 BotFollowerId = follow.BotFollowerId,
                                 UserFollowedId = follow.UserFollowedId,
                                 BotFollowedId = follow.BotFollowedId,
                                 UserFollower = follow.UserFollower,
                                 BotFollower = follow.BotFollower,
                                 UserFollowed = follow.UserFollowed,
                                 BotFollowed = follow.BotFollowed
                             });

                return await follows.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFollowModulesForUserAsFollowedAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task<List<Follow>> GetFollowModulesForBotAsFollowerAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.BotFollowerId == id)
                             .Skip(startInterval)
                             .Take(endInterval - startInterval)
                             .Select(follow => new Follow
                             {
                                 FollowId = follow.FollowId,
                                 UserFollowerId = follow.UserFollowerId,
                                 BotFollowerId = follow.BotFollowerId,
                                 UserFollowedId = follow.UserFollowedId,
                                 BotFollowedId = follow.BotFollowedId,
                                 UserFollower = follow.UserFollower,
                                 BotFollower = follow.BotFollower,
                                 UserFollowed = follow.UserFollowed,
                                 BotFollowed = follow.BotFollowed
                             });

                return await follows.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFollowModulesForBotAsFollowerAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Follow>> GetFollowModulesForBotAsFollowedAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                var follows = _context.Follows.Where(follow => follow.BotFollowedId == id)
                             .Skip(startInterval)
                             .Take(endInterval - startInterval)
                             .Select(follow => new Follow
                             {
                                 FollowId = follow.FollowId,
                                 UserFollowerId = follow.UserFollowerId,
                                 BotFollowerId = follow.BotFollowerId,
                                 UserFollowedId = follow.UserFollowedId,
                                 BotFollowedId = follow.BotFollowedId,
                                 UserFollower = follow.UserFollower,
                                 BotFollower = follow.BotFollower,
                                 UserFollowed = follow.UserFollowed,
                                 BotFollowed = follow.BotFollowed
                             });

                return await follows.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFollowModulesForBotAsFollowedAsync with BotId {BotId}", id);
                throw;
            }
        }
    }
}
