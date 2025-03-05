using System.Security.Claims;
using _1_BusinessLayer.Abstractions.AbstractServices;
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
        [Authorize]
        [HttpPatch("User/Settings/EditProfile")]
        public async Task<IActionResult> EditProfile([FromBody] UserEditProfileDto userEditProfileDto)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _userService.EditProfile(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value),userEditProfileDto);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);

        }

        [Authorize]
        [HttpPatch("User/Settings/EditPreferences")]
        public async Task<IActionResult> EditPreferences([FromBody] UserEditPreferencesDto userEditPreferencesDto)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _userService.EditPreferences(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value),userEditPreferencesDto);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("User/Settings/Security/ChangePassword")]
        public async Task<IActionResult> ChangePassword(string oldPassword ,string newPassword)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _userService.ChangePassword(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value),oldPassword,newPassword);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("User/Settings/Security/ChangeEmail")]
        public async Task<IActionResult> ChangeEmail(string changeEmailToken, string newEmail)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _userService.ChangeEmail(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), newEmail, changeEmailToken);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("User/Settings/Security/ChangeUsername")]
        public async Task<IActionResult> ChangeUsername(string oldUsername, string newUsername)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _userService.ChangeUsername(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value),oldUsername,newUsername);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("User/Settings/Security/TwoFactorActivation")]
        public async Task<IActionResult> ActivateTwoFactor()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _userService.ActivateTwoFactorAuthentication(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);

        }

        [Authorize]
        [HttpPatch("User/Settings/Security/TwoFactorDeactivation")]
        public async Task<IActionResult> DeactivateTwoFactor()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _userService.DisableTwoFactorAuthentication(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);

        }

        [Authorize]
        [HttpPatch("User/Settings/Security/AddPhoneNumber")]
        public async Task<IActionResult> ConfirmPhoneNumber(string confirmPhoneNumberToken)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _userService.ConfirmPhoneNumber((int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)), confirmPhoneNumberToken);
#pragma warning restore CS8604 // Possible null reference argument.
            return Ok(result);

        }


        [Authorize]
        [HttpPost("User/Settings/Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _userService.Logout();
            return Ok(result);
        }


    }
}
