using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using _1_BusinessLayer.Abstractions.AbstractServices;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum/Post")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly AbstractEntryService _entryService;
        public EntryController(AbstractEntryService entryService)
        {
            _entryService = entryService;
        }

        [Authorize]
        [HttpPost("Post/{postId}/Entries")]
        public async Task<IActionResult> CreateEntry(string context, int postId)
        {
            var result = await _entryService.CreateEntry(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), postId, context);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("Post/{postId}/Entries/{entryId}")]
        public async Task<IActionResult> DeleteEntry(int entryId)
        {
            var result = await _entryService.DeleteEntry(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), entryId);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("Post/{postId}/Entries/{entryId}")]
        public async Task<IActionResult> UpdateEntry(string context, int entryId)
        {
            var result = await _entryService.UpdateEntry(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), entryId, context);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("Post/{postId}/Entries/{entryId}/Like")]
        public async Task<IActionResult> LikeEntry(string context, int entryId)
        {
            var result = await _entryService.UpdateEntry(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), entryId, context);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("Post/{postId}/Entries/{entryId}/Unlike")]
        public async Task<IActionResult> UnlikeEntry(string context, int entryId)
        {
            var result = await _entryService.UpdateEntry(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), entryId, context);
            return Ok(result);
        }
    }
}
