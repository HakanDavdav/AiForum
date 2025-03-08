using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class EntryService : AbstractEntryService
    {
        public EntryService(AbstractEntryRepository entryRepository, AbstractLikeRepository likeRepository) : base(entryRepository, likeRepository)
        {
        }

        public override Task<IdentityResult> CreateComplaint(int userId, int entryId)
        {
            throw new NotImplementedException();
        }

        public override async Task<IdentityResult> CreateEntry(int userId, int postId, string context)
        {
            await _entryRepository.InsertAsync(new Entry
            {
                UserId = userId,
                PostId = postId,
                Context = context
            });
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeleteEntry(int userId, int entryId)
        {
            var entry = await _entryRepository.GetByIdWithInfoAsync(entryId);
            if (entry.UserId == userId)
            {
                await _entryRepository.DeleteAsync(entry);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("Unauthorized deletion"));
        }

        public override async Task<IdentityResult> LikeEntry(int userId, int entryId)
        {
            await _likeRepository.InsertAsync(new Like
            {
                UserId = userId,
                EntryId = entryId
            });
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> UnlikeEntry(int userId, int likeId)
        {
            var like = await _likeRepository.GetByIdWithInfoAsync(likeId);
            if (like.UserId == userId)
            {
                await _likeRepository.DeleteAsync(like);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("Unauthorized deletion"));
        }

        public override async Task<IdentityResult> UpdateEntry(int userId, int entryId, string context)
        {
            var entry = await _entryRepository.GetByIdWithInfoAsync(entryId);
            if (entry.UserId == userId)
            {
                entry.Context = context;
                await _entryRepository.UpdateAsync(entry);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("Unauthorized deletion"));
        }

    }
}
