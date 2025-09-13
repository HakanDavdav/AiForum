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
    public class EntryRepository : AbstractEntryRepository
    {
        public EntryRepository(ApplicationDbContext context, ILogger<Entry> logger) : base(context, logger)
        {
        }


        public override async Task<Entry> GetEntryModuleAsync(int id)
        {
            var entry = await _context.Entries.Where(entry => entry.EntryId == id).Select(
                entry => new Entry
                {
                    EntryId = entry.EntryId,
                    Context = entry.Context,
                    DateTime = entry.DateTime,
                    OwnerUserId = entry.OwnerUserId,
                    OwnerBotId = entry.OwnerBotId,
                    PostId = entry.PostId,
                    OwnerUser = entry.OwnerUser,
                    OwnerBot = entry.OwnerBot,
                    Post = entry.Post,
                    Likes = entry.Likes.Take(10).Select(entry => new Like
                    {
                        LikeId = entry.LikeId,
                        DateTime = entry.DateTime,
                        OwnerUserId = entry.OwnerUserId,
                        OwnerBotId = entry.OwnerBotId,
                        PostId = entry.PostId,
                        EntryId = entry.EntryId,
                        OwnerUser = entry.OwnerUser,
                        OwnerBot = entry.OwnerBot,
                        Post = entry.Post,
                    }).ToList(),
                }).FirstOrDefaultAsync();
            return entry;
        }

        public override async Task<List<Entry>> GetEntryModulesForBotAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                return await _context.Entries
                    .Where(entry => entry.OwnerBotId == id)
                    .OrderByDescending(entry => entry.DateTime)
                    .Skip(startInterval)
                    .Take(endInterval - startInterval)
                    .Select(entry => new Entry
                    {
                        EntryId = entry.EntryId,
                        Context = entry.Context,
                        DateTime = entry.DateTime,
                        OwnerUserId = entry.OwnerUserId,
                        OwnerBotId = entry.OwnerBotId,
                        PostId = entry.PostId,
                        OwnerUser = entry.OwnerUser,
                        OwnerBot = entry.OwnerBot,
                        Post = entry.Post,
                        Likes = entry.Likes.Take(10).Select(entry => new Like
                        {
                            LikeId = entry.LikeId,
                            DateTime = entry.DateTime,
                            OwnerUserId = entry.OwnerUserId,
                            OwnerBotId = entry.OwnerBotId,
                            PostId = entry.PostId,
                            EntryId = entry.EntryId,
                            OwnerUser = entry.OwnerUser,
                            OwnerBot = entry.OwnerBot,
                            Post = entry.Post,
                        }).ToList(),
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in GetEntryModulesForBotAsync with ParentBotId {ParentBotId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetEntryModulesForPostAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                return await _context.Entries
                    .Where(entry => entry.PostId == id)
                    .OrderByDescending(entry => entry.DateTime)
                    .Skip(startInterval)
                    .Take(endInterval - startInterval)
                    .Select(entry => new Entry
                    {
                        EntryId = entry.EntryId,
                        Context = entry.Context,
                        DateTime = entry.DateTime,
                        OwnerUserId = entry.OwnerUserId,
                        OwnerBotId = entry.OwnerBotId,
                        PostId = entry.PostId,
                        OwnerUser = entry.OwnerUser,
                        OwnerBot = entry.OwnerBot,
                        Post = entry.Post,
                        Likes = entry.Likes.Take(10).Select(entry => new Like
                        {
                            LikeId = entry.LikeId,
                            DateTime = entry.DateTime,
                            OwnerUserId = entry.OwnerUserId,
                            OwnerBotId = entry.OwnerBotId,
                            PostId = entry.PostId,
                            EntryId = entry.EntryId,
                            OwnerUser = entry.OwnerUser,
                            OwnerBot = entry.OwnerBot,
                            Post = entry.Post,
                        }).ToList(),
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in GetEntryModulesForPostAsync with PostId {PostId}", id);
                throw;
            }
        }

        public override async Task<List<Entry>> GetEntryModulesForUserAsync(int id, int startInterval, int endInterval)
        {
            try
            {
                return await _context.Entries
                    .Where(entry => entry.OwnerUserId == id)
                    .OrderByDescending(entry => entry.DateTime)
                    .Skip(startInterval)
                    .Take(endInterval - startInterval)
                    .Select(entry => new Entry
                    {
                        EntryId = entry.EntryId,
                        Context = entry.Context,
                        DateTime = entry.DateTime,
                        OwnerUserId = entry.OwnerUserId,
                        OwnerBotId = entry.OwnerBotId,
                        PostId = entry.PostId,
                        OwnerUser = entry.OwnerUser,
                        OwnerBot = entry.OwnerBot,
                        Post = entry.Post,
                        Likes = entry.Likes.Take(10).Select(entry => new Like
                        {
                            LikeId = entry.LikeId,
                            DateTime = entry.DateTime,
                            OwnerUserId = entry.OwnerUserId,
                            OwnerBotId = entry.OwnerBotId,
                            PostId = entry.PostId,
                            EntryId = entry.EntryId,
                            OwnerUser = entry.OwnerUser,
                            OwnerBot = entry.OwnerBot,
                            Post = entry.Post,
                        }).ToList(),
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in GetEntryModulesForUserAsync with ParentUserId {ParentUserId}", id);
                throw;
            }
        }

    }
}
