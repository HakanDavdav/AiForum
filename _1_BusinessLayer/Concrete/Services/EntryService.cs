using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class EntryService : AbstractEntryService
    {
        public EntryService(AbstractEntryRepository entryRepository, AbstractUserRepository userRepository) : base(entryRepository, userRepository)
        {
        }

        public override async Task<IdentityResult> CreateEntryAsync(int userId, int postId, CreateEntryDto createEntryDto)
        {
            var entry = createEntryDto.CreateEntryDto_To_Entry(userId);
            await _entryRepository.InsertAsync(entry);
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeleteEntryAsync(int userId, int entryId)
        {
            var entry = await _entryRepository.GetByIdAsync(entryId);
            if(entry.UserId == userId)
            {
                await _entryRepository.InsertAsync(entry);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("You cannot delete another user's entry"));
        }

        public override async Task<IdentityResult> EditEntryAsync(int userId, EditEntryDto editEntryDto)
        {
            var entry = await _entryRepository.GetByIdAsync(editEntryDto.EntryId);
            if (entry != null)
            {
                if (entry.UserId == userId)
                {
                    entry = editEntryDto.Update___EditEntryDto_To_Entry(entry);
                    await _entryRepository.UpdateAsync(entry);
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnauthorizedError("You cannot edit another user's entry"));
            }
            return IdentityResult.Failed(new UnauthorizedError("You cannot edit another user's entry"));
        }

        public override async Task<ObjectIdentityResult<EntryProfileDto>> GetEntryAsync(int entryId)
        {
            var listEntry = await _entryRepository.GetWithCustomSearchAsync(query => query.Where(entry => entry.EntryId == entryId)
            .Select(entry => new Entry
            {
                EntryId = entry.EntryId,
                UserId = entry.UserId,
                PostId = entry.PostId,
                DateTime = entry.DateTime,
                Context = entry.Context,
                User = entry.User,
            }));
            var entry = listEntry.FirstOrDefault();
            if (entry != null)
            {
                var entryProfileDto = entry.Entry_To_EntryProfileDto();
                return ObjectIdentityResult<EntryProfileDto>.Succeded(entryProfileDto);
            }
            return ObjectIdentityResult<EntryProfileDto>.Failed(null, new IdentityError[]{ new NotFoundError("Entry not found") });
        }
    }
}
