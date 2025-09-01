using System.Security.Claims;
using _1_BusinessLayer.Abstractions.AbstractServices;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var IdentityResult = await _postService.CreatePost(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), createPostDto);
                return IdentityResult.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            try
            {
                var result = await _postService.DeletePost(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), postId);
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }   
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("{postId}")]
        public async Task<IActionResult> EditPost([FromBody] EditPostDto editPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _postService.EditPost(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), editPostDto);
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("{postId}/Like")]
        public async Task<IActionResult> LikePost(int postId)
        {
            try
            {
                var result = await _likeService.LikePost(postId, int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("{postId}/like/{likeId}")]
        public async Task<IActionResult> UnlikePost(int likeId)
        {
            try
            {
                var result = await _likeService.Unlike(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), likeId);
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }
        
         
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(int postId,int page)
        {
            try
            {
                var objectResult = await _postService.GetPostAsync(postId, page, HttpContext.User.FindFirst("ENTRY PER PAGE").Value);
                return objectResult.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }

        [HttpGet("TrendPosts")]
        public async Task<IActionResult> GetTrendPosts(DateTime date)
        {
            try
            {
                var objectResult = await _postService.GetTrendingPosts(TODO);
                return objectResult.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }



        [HttpGet("MostLikedPosts")]
        public async Task<IActionResult> GetMostLikedPosts(DateTime date)
        {
            try
            {
                var objectResult = await _postService.GetMostLikedPosts(date, TODO);
                return objectResult.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }


    }
}
