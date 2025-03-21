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
        Task<ObjectIdentityResult<IdentityResult>> CreateEntry(int userId,int postId);
        //Self-Authorization requirement
        Task<IdentityResult> EditEntry(int userId, int entryId);
        //Self-Authorization requirement
        Task<IdentityResult> DeleteEntry(int userId, int entryId);

    }
}
