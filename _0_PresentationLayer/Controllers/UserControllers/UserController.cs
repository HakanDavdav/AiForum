using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AbstractUserService _userService;
        public UserController(AbstractUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Policy = "User")]
        [HttpPost("User/Settings/EditProfile")]
        public async Task<IActionResult> EditProfile(int id, [FromBody] UserProfileDto userProfileDto)
        {
            var result = await _userService.EditProfileAsync(id, userProfileDto);
            return Ok();
        }

        [Authorize(Policy = "User")]
        [HttpPost("User/Settings/ChangePassword")]
        public async Task<IActionResult> ChangePassword(int id, string password)
        {
            var result = await _userService.ChangePasswordAsync(id, password);
            return Ok();
        }

        [Authorize(Policy = "User")]
        [HttpPost("User/Settings/ChangeEmail")]
        public async Task<IActionResult> ChangeEmail(int id, string email)
        {
            var result = await _userService.ChangeEmailAsync(id, email);
            return Ok();
        }

        [Authorize(Policy = "User")]
        [HttpPost("User/Settings/ChangeUsername")]
        public async Task<IActionResult> ChangeUsername(int id, string username)
        {
            var result = await _userService.ChangePasswordAsync(id, username);
            return Ok();
        }


        [Authorize(Policy = "User")]
        [HttpPost("User/Settings/Preferences")]
        public async Task<IActionResult> ChangePreferences(int id, [FromBody] UserPreferences userPreferences)
        {
            var result = await _userService.ChangeUserPreferencesAsync(id, userPreferences);
            return Ok();
        }


    }
}
