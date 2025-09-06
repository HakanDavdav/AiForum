using System.Security.Claims;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum/Posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AbstractPostService _postService;
        private readonly AbstractLikeService _likeService;
        public PostController(AbstractPostService postService, AbstractLikeService likeService)
        {
            _postService = postService;
            _likeService = likeService;
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody]CreatePostDto createPostDto)
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
  
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("{postId}")]
        public async Task<IActionResult> EditPost([FromBody] EditPostDto editPostDto)
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("{postId}/Like")]
        public async Task<IActionResult> LikePost(int postId)
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("{postId}/like/{likeId}")]
        public async Task<IActionResult> UnlikePost(int likeId)
        {

        }
        
         
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(int postId,int page)
        {

        }

        [HttpGet("TrendPosts")]
        public async Task<IActionResult> GetTrendPosts(DateTime date)
        {

        }



        [HttpGet("MostLikedPosts")]
        public async Task<IActionResult> GetMostLikedPosts(DateTime date)
        {

        }


    }
}
