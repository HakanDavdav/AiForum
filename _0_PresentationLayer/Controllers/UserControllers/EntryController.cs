using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly AbstractEntryService _entryService;
        private readonly AbstractLikeService _likeService;
        private readonly AbstractUserService _userService;
        public EntryController(AbstractEntryService entryService, AbstractLikeService likeService, AbstractUserService userService)
        {
            _entryService = entryService;
            _likeService = likeService;
            _userService = userService;
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("Posts/{postId}/Entries")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryDto createEntryDto, int postId)
        {
            var result = await _entryService.CreateEntryAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!), postId, createEntryDto);
            return result.ResultWrapErrorCode();
            
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("Posts/{postId}/Entries/{entryId}")]
        public async Task<IActionResult> DeleteEntry(int entryId)
        {
            var result = await _entryService.DeleteEntryAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!), entryId);
            return result.ResultWrapErrorCode();
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("Posts/{postId}/Entries/{entryId}")]
        public async Task<IActionResult> EditEntry([FromBody] EditEntryDto editEntryDto)
        {
            var result = await _entryService.EditEntryAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!), editEntryDto);
            return result.ResultWrapErrorCode();
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("Posts/{postId}/Entries/{entryId}/Likes")]
        public async Task<IActionResult> LikeEntry(int entryId)
        {
            var result = await _likeService.LikeEntry(entryId, int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!));
            return result.ResultWrapErrorCode();
        }


        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("Posts/{postId}/Entries/{entryId}/Likes/{likeId}")]
        public async Task<IActionResult> UnlikeEntry(int likeId)
        {
            var result = await _likeService.UnlikeEntry(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!), likeId);
            return result.ResultWrapErrorCode();
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpGet("Users/{userId}/Likes/{page}")]
        public async Task<IActionResult> LoadProfileLikes(int userId, int page)
        {
            var result = await _userService.LoadProfileLikes(userId, page);
            return result.ResultWrapErrorCode();

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpGet("Users/{userId}/Likes/{page}")]
        public async Task<IActionResult> LoadPostLikes(int userId, int page)
        {
            var result = await _userService.LoadProfileLikes(userId, page);
            return result.ResultWrapErrorCode();

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpGet("Users/{userId}/Likes/{page}")]
        public async Task<IActionResult> LoadEntryLikes(int userId, int page)
        {
            var result = await _userService.LoadProfileLikes(userId, page);
            return result.ResultWrapErrorCode();

        }
    }
}
