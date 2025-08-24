using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IPostService
    {

        //Self-Authorization requirement
        Task<IdentityResult> CreatePost(int userId,CreatePostDto createPostDto);
        //Self-Authorization requirement
        Task<IdentityResult> EditPost(int userId,EditPostDto editPostDto);
        //Self-Authorization requirement
        Task<IdentityResult> DeletePost(int userId,int postId);
        //Self-Preference check
        Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(int entryPerPagePreference, DateTime date);
        //Self-Preference check
        Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(int postPerPagePreference, DateTime date);
        Task<ObjectIdentityResult<PostDto>> GetPostAsync(int postId, int page, int entryPerPagePreference);



    }
}
