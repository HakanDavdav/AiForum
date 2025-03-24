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

        Task<IdentityResult> LikeEntry(int entryId, int userId);
        Task<IdentityResult> LikePost(int postId, int userId);
        Task<IdentityResult> Unlike(int userId, int likeId);

    }
}
