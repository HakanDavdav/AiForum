using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface ILikeService
    {
        Task<IdentityResult> LikePost(int postId);
        Task<IdentityResult> UnlikePost(int postId);
        Task<IdentityResult> LikeEntry(int entryId);
        Task<IdentityResult> UnlikeEntry(int entryId);

    }
}
