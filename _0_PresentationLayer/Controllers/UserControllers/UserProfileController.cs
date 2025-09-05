using System.Security.Claims;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum/OwnerUser")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly AbstractUserService userService;
        public UserProfileController(AbstractUserService profileService)
        {
            userService = profileService;
        }

        [Authorize(Policy = "TempUserPolicy")]
        [HttpPatch("You")]
        public async Task<IActionResult> CreateUserProfile([FromBody]UserCreateProfileDto userCreateProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await userService.CreateProfileAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), userCreateProfileDto);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("You/EditProfile")]
        public async Task<IActionResult> EditProfile([FromBody] UserEditProfileDto userEditProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

#pragma warning disable CS8604 // Possible null reference argument.
            var result = await userService.EditProfile(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), userEditProfileDto);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("You/EditPreferences")]
        public async Task<IActionResult> EditPreferences([FromBody] UserEditPreferencesDto userEditPreferencesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

#pragma warning disable CS8604 // Possible null reference argument.
            var result = await userService.EditPreferences(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), userEditPreferencesDto);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpGet("You/BotPanel")]
        public async Task<IActionResult> GetBotPanel()
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await userService.GetBotPanel(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
#pragma warning restore CS8604 // Possible null reference argument.
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpGet("You")]
        public async Task<IActionResult> GetYourUserProfile()
        {
            try
            {
                var entryPerPageClaim = HttpContext.User.FindFirst("ENTRY PER PAGE");
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await userService.GetUserProfile(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), TODO);
#pragma warning restore CS8604 // Possible null reference argument.
                return Ok(result);
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();
            }
        }
       
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var entryPerPageClaim = HttpContext.User.FindFirst("ENTRY PER PAGE");
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await userService.GetUserProfile(userId, TODO);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }

        [HttpGet("{userId}/ReloadEntries")]
        public async Task<IActionResult> ReloadProfileEntries(int userId,int startInterval, int endInterval)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await userService.ReloadProfileEntries(userId, startInterval, endInterval);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }

        [HttpGet("{userId}/ReloadPosts")]
        public async Task<IActionResult> ReloadProfilePosts(int userId, int startInterval, int endInterval )
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await userService.ReloadProfilePosts(userId, startInterval, endInterval);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }


        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await userService.DeleteUser(userId);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }

    }
}
