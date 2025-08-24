using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IEntryService
    {
        //Self-Authorization requirement
        Task<IdentityResult> CreateEntryAsync(int userId,int postId,CreateEntryDto createEntryDto);
        //Self-Authorization requirement
        Task<IdentityResult> EditEntryAsync(int userId, EditEntryDto editEntryDto);
        //Self-Authorization requirement
        Task<IdentityResult> DeleteEntryAsync(int userId, int entryId);
        Task<ObjectIdentityResult<EntryProfileDto>> GetEntryAsync(int entryId);

    }
}
