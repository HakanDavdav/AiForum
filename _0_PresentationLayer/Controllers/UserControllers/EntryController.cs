using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;

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

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("{postId}/Entries/{entryId}")]
        public async Task<IActionResult> DeleteEntry(int entryId)
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("{postId}/Entries/{entryId}")]
        public async Task<IActionResult> EditEntry([FromBody] EditEntryDto editEntryDto)
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("{postId}/Entries/{entryId}/Like")]
        public async Task<IActionResult> LikeEntry(int entryId)
        {

        }


        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("{postId}/Entries/{entryId}/{likeId}")]
        public async Task<IActionResult> UnlikeEntry(int likeId)
        {

        }
    }
}
