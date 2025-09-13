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
                _logger.LogError(ex, "Error in GetFollowModulesForUserAsFollowerAsync with ParentUserId {ParentUserId}", id);
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
                _logger.LogError(ex, "Error in GetFollowModulesForUserAsFollowedAsync with ParentUserId {ParentUserId}", id);
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
                _logger.LogError(ex, "Error in GetFollowModulesForBotAsFollowerAsync with ParentBotId {ParentBotId}", id);
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
                _logger.LogError(ex, "Error in GetFollowModulesForBotAsFollowedAsync with ParentBotId {ParentBotId}", id);
                throw;
            }
        }

    }
}
