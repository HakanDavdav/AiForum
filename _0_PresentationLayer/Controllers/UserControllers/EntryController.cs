using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum/Posts")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly AbstractEntryService _entryService;
        private readonly AbstractLikeService _likeService;
        public EntryController(AbstractEntryService entryService, AbstractLikeService likeService)
        {
            _entryService = entryService;
            _likeService = likeService;
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("{postId}/Entries")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryDto createEntryDto, int postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            try
            {
                var IdentityResult = await _entryService.CreateEntryAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), postId, createEntryDto);
                return IdentityResult.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("{postId}/Entries/{entryId}")]
        public async Task<IActionResult> DeleteEntry(int entryId)
        {
            try
            {
                var IdentityResult = await _entryService.DeleteEntryAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), entryId);
                return IdentityResult.ResultWrapErrorCode();
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("{postId}/Entries/{entryId}")]
        public async Task<IActionResult> EditEntry([FromBody] EditEntryDto editEntryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var IdentityResult = await _entryService.EditEntryAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), editEntryDto);
                return IdentityResult.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("{postId}/Entries/{entryId}/Likes")]
        public async Task<IActionResult> LikeEntry(int entryId)
        {
            try
            {
                var IdentityResult = await _likeService.LikeEntry(entryId, int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
                return IdentityResult.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }


        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("{postId}/Entries/{entryId}/{likeId}")]
        public async Task<IActionResult> UnlikeEntry(int likeId)
        {
            try
            {
                var IdentityResult = await _likeService.Unlike(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), likeId);
                return IdentityResult.ResultWrapErrorCode();
            }
            catch (Exception e)
            {

                return e.ExceptionWrapErrorCode();
            }
        }
    }
}
