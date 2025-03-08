using System.Security.Claims;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum")]
    [ApiController]
    public class UserInteractionController : ControllerBase
    {
        private readonly AbstractInteractionService _interactionService;
        public UserInteractionController(AbstractInteractionService interactionService)
        {
            _interactionService = interactionService;
        }
        [Authorize]
        [HttpPost("Post/{postId}/Entries")]
        public async Task<IActionResult> CreateEntry(string context, int postId)
        {
            var result = await _interactionService.CreateEntry(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), postId, context);
            return Ok(result); 
        }

        // Delete an entry for a post
        [Authorize]
        [HttpDelete("Post/{postId}/Entries/{entryId}")]
        public async Task<IActionResult> DeleteEntry(int entryId)
        {
            var result = await _interactionService.DeleteEntry(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), entryId);
            return Ok(result);
        }

        // Update an existing entry for a post
        [Authorize]
        [HttpPatch("Post/{postId}/Entries/{entryId}")]
        public async Task<IActionResult> UpdateEntry(string context, int entryId)
        {
            var result = await _interactionService.UpdateEntry(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value),entryId, context);
            return Ok(result); 
        }

        // Create a post
        [Authorize]
        [HttpPost("Post")]
        public async Task<IActionResult> CreatePost(string context,string title, int postId)
        {
            var result = await _interactionService.CreatePost(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value),title,context);
            return Ok(result);
        }

        // Delete a post
        [Authorize]
        [HttpDelete("Post/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var result = await _interactionService.DeletePost(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), postId);
            return Ok(result);        
        }

        // Edit a post
        [Authorize]
        [HttpPatch("Post/{postId}")]
        public async Task<IActionResult> UpdatePost(string context, string title, int postId)
        {
            var result = await _interactionService.UpdatePost(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), postId, title, context);
            return Ok(result); 
        }

    }
}
