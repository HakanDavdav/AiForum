using System.Security.Claims;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum/Post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AbstractPostService _postService;
        public PostController(AbstractPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(string context,string title, int postId)
        {
            
            var result = await _postService.CreatePost(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value),title,context);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var result = await _postService.DeletePost(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value),postId);
            return Ok(result);        
        }

        [Authorize]
        [HttpPatch("{postId}")]
        public async Task<IActionResult> UpdatePost(string context, string title, int postId)
        {
            var result = await _postService.UpdatePost(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value),postId,title,context);
            return Ok(result); 
        }

        [Authorize]
        [HttpPost("{postId}/like")]
        public async Task<IActionResult> LikePost(int postId)
        {
            var result = await _postService.LikePost(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), postId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("{postId}/Unlike")]
        public async Task<IActionResult> UnlikePost(int postId)
        {
            var result = await _postService.UnlikePost(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), postId);
            return Ok(result);
        }
        
        [Authorize]
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var result = await _postService
            return Ok(result);
        }


    }
}
