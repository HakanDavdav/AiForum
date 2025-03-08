using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IEntryService
    {
        Task<IdentityResult> CreateEntry(int userId, int postId, string context);
        Task<IdentityResult> UpdateEntry(int userId, int postId, string context);
        Task<IdentityResult> DeleteEntry(int userId, int entryId);
        Task<IdentityResult> LikeEntry(int userId, int entryId);
        Task<IdentityResult> UnlikeEntry(int userId, int entryId);
        Task<IdentityResult> CreateComplaint(int userId, int entryId);

    }
}
