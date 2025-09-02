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

        public override async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public override async Task DeleteAsync(Entry t)
        {
            try
            {
                _context.Entries.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in DeleteAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
        }


        public override async Task<Entry> GetByIdAsync(int id)
        {
            try
            {
#pragma warning disable CS8603 // Possible null reference return.
                return await _context.Entries.FirstOrDefaultAsync(entry => entry.EntryId == id);    
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in GetByIdAsync with EntryId {EntryId}", id);
                throw;
            }
        }

        public override async Task ManuallyInsertAsync(Entry t)
        {
            try
            {
                await _context.Entries.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in ManuallyInsertAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
        }

        public override async Task UpdateAsync(Entry t)
        {
            try
            {
                _context.Entries.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repo error in UpdateAsync for EntryId {EntryId}", t.EntryId);
                throw;
            }
        }

        public override async Task<List<Entry>> GetWithCustomSearchAsync(Func<IQueryable<Entry>, IQueryable<Entry>> queryModifier)
        {
            IQueryable<Entry> query = _context.Entries;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }

        public override async Task<Entry> GetBySpecificPropertySingularAsync(Func<IQueryable<Entry>, IQueryable<Entry>> queryModifier)
        {
            IQueryable<Entry> query = _context.Entries;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
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
                _logger.LogError(ex, "Repo error in GetEntryModulesForBotAsync with OwnerBotId {OwnerBotId}", id);
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
                _logger.LogError(ex, "Repo error in GetEntryModulesForUserAsync with OwnerUserId {OwnerUserId}", id);
                throw;
            }
        }
    }
}
