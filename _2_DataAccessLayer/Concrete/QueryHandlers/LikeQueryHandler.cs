using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.QueryHandlers
{
    public class LikeQueryHandler : AbstractLikeQueryHandler
    {
        public LikeQueryHandler(ILogger<Like> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public override  async Task<List<Like>> GetLikeModulesForUserAsync(int userId, int startInterval, int endInterval)
        {
            try
            {
                var likes = await _commandHandler.Export<Like>()
            .Where(like => like.OwnerUserId == userId)
            .OrderByDescending(like => like.DateTime)
            .Skip(startInterval)
            .Take(endInterval - startInterval)
            .Select(like => new Like
            {
                LikeId = like.LikeId,
                DateTime = like.DateTime,
                OwnerUserId = like.OwnerUserId,
                OwnerBotId = like.OwnerBotId,
                PostId = like.PostId,
                OwnerUser = like.OwnerUser,
                OwnerBot = like.OwnerBot,
                Post = like.Post,
            }).ToListAsync();
                return likes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public override async Task<List<Like>> GetLikeModulesForBotAsync(int botId, int startInterval, int endInterval)
        {
            try
            {
                var likes = await _commandHandler.Export<Like>()
            .Where(like => like.OwnerBotId == botId)
            .OrderByDescending(like => like.DateTime)
            .Skip(startInterval)
            .Take(endInterval - startInterval)
            .Select(like => new Like
            {
                LikeId = like.LikeId,
                DateTime = like.DateTime,
                OwnerUserId = like.OwnerUserId,
                OwnerBotId = like.OwnerBotId,
                PostId = like.PostId,
                OwnerUser = like.OwnerUser,
                OwnerBot = like.OwnerBot,
                Post = like.Post,
            }).ToListAsync();
                return likes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public override async Task<List<Like>> GetLikeModulesForEntryAsync(int entryId, int startInterval, int endInterval)
        {
            try
            {
                var likes = await _commandHandler.Export<Like>()
            .Where(like => like.EntryId == entryId)
            .OrderByDescending(like => like.DateTime)
            .Skip(startInterval)
            .Take(endInterval - startInterval)
            .Select(like => new Like
            {
                LikeId = like.LikeId,
                DateTime = like.DateTime,
                OwnerUserId = like.OwnerUserId,
                OwnerBotId = like.OwnerBotId,
                PostId = like.PostId,
                OwnerUser = like.OwnerUser,
                OwnerBot = like.OwnerBot,
                Post = like.Post,
            }).ToListAsync();
                return likes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public override async Task<List<Like>> GetLikeModulesForPostAsync(int postId, int startInterval, int endInterval)
        {
            try
            {
                var likes = await _commandHandler.Export<Like>()
            .Where(like => like.PostId == postId)
            .OrderByDescending(like => like.DateTime)
            .Skip(startInterval)
            .Take(endInterval - startInterval)
            .Select(like => new Like
            {
                LikeId = like.LikeId,
                DateTime = like.DateTime,
                OwnerUserId = like.OwnerUserId,
                OwnerBotId = like.OwnerBotId,
                PostId = like.PostId,
                OwnerUser = like.OwnerUser,
                OwnerBot = like.OwnerBot,
                Post = like.Post,
            }).ToListAsync();
                return likes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }
    }
}
