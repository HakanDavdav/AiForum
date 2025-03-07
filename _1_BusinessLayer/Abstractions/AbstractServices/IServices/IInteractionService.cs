using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IInteractionService
    {
        Task<IdentityResult> CreatePost();
        Task<IdentityResult> UpdatePost();
        Task<IdentityResult> DeletePost();
        Task<IdentityResult> CreateEntry();
        Task<IdentityResult> UpdateEntry();
        Task<IdentityResult> DeleteEntry();
        Task<IdentityResult> LikePost();
        Task<IdentityResult> UnlikePost();
        Task<IdentityResult> LikeEntry();
        Task<IdentityResult> CreateComplaint();
        Task<IdentityResult> Follow();
        Task<IdentityResult> Unfollow();



    }
}
