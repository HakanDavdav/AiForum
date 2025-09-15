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
    public class EntryQueryHandler : AbstractEntryQueryHandler
    {
        public EntryQueryHandler(ILogger<Entry> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public override async Task<Entry> GetEntryModuleAsync(int id)
        {
            try
            {
                var entry = await _repository.Export<Entry>().Where(entry => entry.EntryId == id).Select(
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public override async Task<List<Entry>> GetEntryModulesForBotAsync(int id, int startInterval, int endInterval)
        {

            try
            {
                return await _repository.Export<Entry>()
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
                _logger.LogError(ex, "");
                throw;
            }

        }

        public override async Task<List<Entry>> GetEntryModulesForPostAsync(int id, int startInterval, int endInterval)
        {

            try
            {
                return await _repository.Export<Entry>()
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
                _logger.LogError(ex, "");
                throw;
            }
        }



        public override async Task<List<Entry>> GetEntryModulesForUserAsync(int id, int startInterval, int endInterval)
        {

            try
            {
                return await _repository.Export<Entry>()
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
                _logger.LogError(ex, "");
                throw;
            }

        }
    }
}
