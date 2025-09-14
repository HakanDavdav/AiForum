using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Abstractions.Interfaces;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.Queries
{
    public class FollowQueries : AbstractFollowQueryHandler
    {
        public FollowQueries(ILogger<Follow> logger, AbstractGenericBaseCommandHandler repository) : base(logger, repository)
        {
        }

        public override async Task<List<Follow>> GetFollowModulesForUserAsFollowerAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                var follows = _repository.Export<Follow>().Where(follow => follow.UserFollowerId == id)
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
                _logger.LogError(ex, "");
                throw;
            }
        }

        public override async Task<List<Follow>> GetFollowModulesForUserAsFollowedAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                var follows = _repository.Export<Follow>().Where(follow => follow.UserFollowedId == id)
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
                _logger.LogError(ex, "");
                throw;
            }
        }

        public override async Task<List<Follow>> GetFollowModulesForBotAsFollowerAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                var follows = _repository.Export<Follow>().Where(follow => follow.BotFollowerId == id)
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
                _logger.LogError(ex, "");
                throw;
            }
        }

        public override async Task<List<Follow>> GetFollowModulesForBotAsFollowedAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                var follows = _repository.Export<Follow>().Where(follow => follow.BotFollowedId == id)
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
                _logger.LogError(ex, "");
                throw;
            }
        }

    }
}
