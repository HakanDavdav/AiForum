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
                return await _context.Follows.AnyAsync(f => f.FollowId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckEntity with FollowId {FollowId}", id);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for FollowId {FollowId}", t.FollowId);
                throw;
            }
        }

        public override async Task<List<Follow>> GetAllByUserIdAsFollowerWithInfoAsync(int id)
        {
            try
            {
                var query = _context.Follows.Where(f => f.UserFollowerId == id)
                                           .Include(f => f.UserFollower)
                                           .Include(f => f.BotFollower)
                                           .Include(f => f.UserFollowed)
                                           .Include(f => f.BotFollowed);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllByUserIdAsFollowerWithInfoAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task<List<Follow>> GetAllByUserIdAsFollowedWithInfoAsync(int id)
        {
            try
            {
                var query = _context.Follows.Where(f => f.UserFollowedId == id)
                                           .Include(f => f.UserFollower)
                                           .Include(f => f.BotFollower)
                                           .Include(f => f.UserFollowed)
                                           .Include(f => f.BotFollowed);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllByUserIdAsFollowedWithInfoAsync with UserId {UserId}", id);
                throw;
            }
        }

        public override async Task<List<Follow>> GetAllByBotIdAsFollowerWithInfoAsync(int id)
        {
            try
            {
                var query = _context.Follows.Where(f => f.BotFollowerId == id)
                                           .Include(f => f.UserFollower)
                                           .Include(f => f.BotFollower)
                                           .Include(f => f.UserFollowed)
                                           .Include(f => f.BotFollowed);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllByBotIdAsFollowerWithInfoAsync with BotId {BotId}", id);
                throw;
            }
        }

        public override async Task<List<Follow>> GetAllByBotIdAsFollowedWithInfoAsync(int id)
        {
            try
            {
                var query = _context.Follows.Where(f => f.BotFollowedId == id)
                                           .Include(f => f.UserFollower)
                                           .Include(f => f.BotFollower)
                                           .Include(f => f.UserFollowed)
                                           .Include(f => f.BotFollowed);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllByBotIdAsFollowedWithInfoAsync with BotId {BotId}", id);
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
                return await _context.Follows.FirstOrDefaultAsync(f => f.FollowId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with FollowId {FollowId}", id);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InsertAsync for FollowId {FollowId}", t.FollowId);
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
    }
}
