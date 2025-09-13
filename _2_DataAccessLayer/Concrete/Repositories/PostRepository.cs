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
    public class PostRepository : AbstractPostRepository
    {
        public PostRepository(ApplicationDbContext context, ILogger<Post> logger) : base(context, logger)
        {
        }


        public override async Task<Post> GetPostModuleAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Posts
                .Where(post => post.PostId == id)
                .Select(post => new Post
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Context = post.Context,
                    DateTime = post.DateTime,
                    OwnerBotId = post.OwnerBotId,
                    OwnerUserId = post.OwnerUserId,
                    OwnerBot = post.OwnerBot,
                    OwnerUser = post.OwnerUser,
                    Likes = post.Likes.Take(10).Select(like => new Like
                    {
                        LikeId = like.LikeId,
                        DateTime = like.DateTime,
                        OwnerUserId = like.OwnerUserId,
                        OwnerBotId = like.OwnerBotId,
                        PostId = like.PostId,
                        OwnerUser = like.OwnerUser,
                        OwnerBot = like.OwnerBot,
                        Post = like.Post,
                    }).ToList(),
                })
                .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task<List<Post>> GetPostModulesForBot(int id, int startInterval, int endInterval)
        {
            try
            {
                return await _context.Posts
                    .Where(post => post.OwnerBotId == id)
                    .OrderByDescending(post => post.DateTime)
                    .Skip(startInterval)
                    .Take(endInterval-startInterval)
                    .Select(post => new Post
                    {
                        PostId = post.PostId,
                        Title = post.Title,
                        Context = post.Context,
                        DateTime = post.DateTime,
                        OwnerBotId = post.OwnerBotId,
                        OwnerUserId = post.OwnerUserId,
                        OwnerBot = post.OwnerBot,
                        OwnerUser = post.OwnerUser,
                        Likes = post.Likes.Take(10).Select(like => new Like
                        {
                            LikeId = like.LikeId,
                            DateTime = like.DateTime,
                            OwnerUserId = like.OwnerUserId,
                            OwnerBotId = like.OwnerBotId,
                            PostId = like.PostId,
                            OwnerUser = like.OwnerUser,
                            OwnerBot = like.OwnerBot,
                            Post = like.Post,
                        }).ToList(),
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEntryModulesForBotAsync with ParentBotId {ParentBotId}", id);
                throw;
            }
        }

        public override async Task<List<Post>> GetPostModulesForUser(int id, int startInterval, int endInterval)
        {
            try
            {
                return await _context.Posts
                    .Where(post => post.OwnerUserId == id)
                    .OrderByDescending(post => post.DateTime)
                    .Skip(startInterval)
                    .Take(endInterval-startInterval)
                    .Select(post => new Post
                    {
                        PostId = post.PostId,
                        Title = post.Title,
                        Context = post.Context,
                        DateTime = post.DateTime,
                        OwnerBotId = post.OwnerBotId,
                        OwnerUserId = post.OwnerUserId,
                        OwnerBot = post.OwnerBot,
                        OwnerUser = post.OwnerUser,
                        Likes = post.Likes.Take(10).Select(like => new Like
                        {
                            LikeId = like.LikeId,
                            DateTime = like.DateTime,
                            OwnerUserId = like.OwnerUserId,
                            OwnerBotId = like.OwnerBotId,
                            PostId = like.PostId,
                            OwnerUser = like.OwnerUser,
                            OwnerBot = like.OwnerBot,
                            Post = like.Post,
                        }).ToList(),
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEntryModulesForUserAsync with ParentUserId {ParentUserId}", id);
                throw;
            }
        }
    }
}
