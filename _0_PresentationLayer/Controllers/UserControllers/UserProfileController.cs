using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum/User")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly AbstractUserService _profileService;
        public UserProfileController(AbstractUserService profileService)
        {
            _profileService = profileService;
        }

        [Authorize]
        [HttpPatch("You")]
        public async Task<IActionResult> CreateUserProfile([FromBody]UserCreateProfileDto userCreateProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _profileService.CreateProfileAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), userCreateProfileDto);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("You/EditProfile")]
        public async Task<IActionResult> EditProfile([FromBody] UserEditProfileDto userEditProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _profileService.EditProfile(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), userEditProfileDto);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);

        }

        [Authorize]
        [HttpPatch("You/EditPreferences")]
        public async Task<IActionResult> EditPreferences([FromBody] UserEditPreferencesDto userEditPreferencesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _profileService.EditPreferences(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), userEditPreferencesDto);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }

        [Authorize]
        [HttpGet("You/BotPanel")]
        public async Task<IActionResult> GetBotPanel()
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _profileService.GetBotPanel(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
#pragma warning restore CS8604 // Possible null reference argument.
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize]
        [HttpGet("You")]
        public async Task<IActionResult> GetYourUserProfile()
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _profileService.GetUserProfile(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
#pragma warning restore CS8604 // Possible null reference argument.
                return Ok(result);
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _profileService.GetUserProfile(userId);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }
    }
}
