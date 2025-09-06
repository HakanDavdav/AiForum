using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices
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
        Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(int postCount);
        //Self-Preference check
        Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(DateTime date, ClaimsPrincipal claims);
        Task<ObjectIdentityResult<PostDto>> GetPostAsync(int postId, ClaimsPrincipal claims);
        Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadPostLikes (int postId, int page);


    }
}
