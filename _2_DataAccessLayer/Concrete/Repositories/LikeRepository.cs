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
    public class LikeRepository : AbstractLikeRepository
    {
        public LikeRepository(ApplicationDbContext context, ILogger<Like> logger) : base(context, logger)
        {
        }

        public override async Task<List<Like>> GetLikeModulesForUser(int userId, int startInterval, int endInterval)
        {
            var likes = await _context.Likes
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

        public override async Task<List<Like>> GetLikeModulesForBot(int botId, int startInterval, int endInterval)
        {
            var likes = await _context.Likes
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

        public override async Task<List<Like>> GetLikeModulesForEntry(int entryId, int startInterval, int endInterval)
        {
            var likes = await _context.Likes
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

        public override async Task<List<Like>> GetLikeModulesForPost(int postId, int startInterval, int endInterval)
        {
            var likes = await _context.Likes
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

    }
}
