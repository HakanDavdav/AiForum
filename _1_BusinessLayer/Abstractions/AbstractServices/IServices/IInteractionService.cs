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
        Task<IdentityResult> CreatePost(int userId,string title,string context);
        Task<IdentityResult> UpdatePost(int userId,int postId,string title,string context);
        Task<IdentityResult> DeletePost(int userId,int postId);
        Task<IdentityResult> LikePost(int userId, int postId);
        Task<IdentityResult> UnlikePost(int userId, int postId);

        Task<IdentityResult> CreateEntry(int userId, int postId,string context);
        Task<IdentityResult> UpdateEntry(int userId, int postId,string context);
        Task<IdentityResult> DeleteEntry(int userId, int entryId);
        Task<IdentityResult> LikeEntry(int userId, int entryId);
        Task<IdentityResult> UnlikeEntry(int userId, int entryId);

        Task<IdentityResult> CreateComplaint(int userId, int? entryId, int? postId);
        Task<IdentityResult> Follow(int userId, int followedUserId);
        Task<IdentityResult> Unfollow(int userId, int followedUserId, int followId);



    }
}
