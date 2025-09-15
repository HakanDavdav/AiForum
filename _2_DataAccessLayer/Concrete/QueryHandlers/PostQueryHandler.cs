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
    public class PostQueryHandler : AbstractPostQueryHandler
    {
        public PostQueryHandler(ILogger<Post> logger, AbstractGenericCommandHandler  repository) : base(logger, repository)
        {
        }

        public override async Task<List<Post>> GetPostModulesForUserAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                return await _commandHandler.Export<Post>()
                    .Where(post => post.OwnerUserId == id)
                    .OrderByDescending(post => post.DateTime)
                    .Skip(startInterval)
                    .Take(endInterval - startInterval)
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


        public override async Task<List<Post>> GetPostModulesForBotAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                return await _commandHandler.Export<Post>()
                    .Where(post => post.OwnerBotId == id)
                    .OrderByDescending(post => post.DateTime)
                    .Skip(startInterval)
                    .Take(endInterval - startInterval)
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


        public override async Task<Post> GetPostModuleAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _commandHandler.Export<Post>()
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

    }
}
