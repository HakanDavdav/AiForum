using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;

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
        public async Task<IActionResult> CreateEntry(CreateEntryDto createEntryDto, int postId)
        {
            try
            {
                var IdentityResult = await _entryService.CreateEntry(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), postId, createEntryDto);
                return IdentityResult.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize]
        [HttpDelete("Post/{postId}/Entries/{entryId}")]
        public async Task<IActionResult> DeleteEntry(int entryId)
        {
             
        }

        [Authorize]
        [HttpPatch("Post/{postId}/Entries/{entryId}")]
        public async Task<IActionResult> EditEntry(EditEntryDto editEntryDto)
        {
             
        }

        [Authorize]
        [HttpPost("Post/{postId}/Entries/{entryId}/Like")]
        public async Task<IActionResult> LikeEntry(int entryId)
        {
             
        }

        [Authorize]
        [HttpPost("Post/{postId}/Entries/{entryId}/Unlike")]
        public async Task<IActionResult> UnlikeEntry(int entryId)
        {
             
        }
    }
}
