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


        public override async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public override async Task DeleteAsync(Post t)
        {
            try
            {
                _context.Posts.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for PostId {PostId}", t.PostId);
                throw;
            }
        }

        public override async Task<Post> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null) return null;
                return await _context.Posts.FirstOrDefaultAsync(post => post.PostId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with PostId {PostId}", id);
                throw;
            }
        }


        public override async Task ManuallyInsertAsync(Post t)
        {
            try
            {
                await _context.Posts.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ManuallyInsertAsync for Post with Title {Title}", t.Title);
                throw;
            }
        }

        public override async Task UpdateAsync(Post t)
        {
            try
            {
                _context.Posts.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for PostId {PostId}", t.PostId);
                throw;
            }
        }

        public override async Task<List<Post>> GetWithCustomSearchAsync(Func<IQueryable<Post>, IQueryable<Post>> queryModifier)
        {
            IQueryable<Post> query = _context.Posts;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }


        public override async Task<Post> GetBySpecificPropertySingularAsync(Func<IQueryable<Post>, IQueryable<Post>> queryModifier)
        {
            IQueryable<Post> query = _context.Posts;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
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

        public override async Task ManuallyInsertRangeAsync(List<Post> posts)
        {
            _context.Posts.AddRange(posts);
            await _context.SaveChangesAsync();
        }
    }
}
