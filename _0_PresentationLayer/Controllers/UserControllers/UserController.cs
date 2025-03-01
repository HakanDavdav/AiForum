using System.Security.Claims;
using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;

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
        [Authorize(Policy = "StandardUser")]
        [HttpPatch("User/Profile/EditProfile")]
        public async Task<IActionResult> EditProfile([FromBody] UserEditProfileDto userEditProfileDto)
        {
            var result = await _userService.EditProfile(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), userEditProfileDto);
            return Ok(result);
            
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPatch("User/Settings/Password")]
        public async Task<IActionResult> ChangePassword(string oldPassword ,string newPassword)
        {
            var result = await _userService.ChangePassword(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), oldPassword, newPassword);
            return Ok(result);
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPatch("User/Settings/Email")]
        public async Task<IActionResult> ChangeEmail(string changeEmailToken, string newEmail)
        {
            var result = await _userService.ChangeEmail(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),newEmail,changeEmailToken);
            return Ok(result);
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPatch("User/Settings/Username")]
        public async Task<IActionResult> ChangeUsername(string newUsername)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPatch("User/Settings/PhoneNumber")]
        public async Task<IActionResult> ChangePhoneNumber(string newPhoneNumber)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPatch("User/Settings/PhoneNumberConfirmation")]
        public async Task<IActionResult> ConfirmPhoneNumber(string changePhoneNumberToken)
        {
            throw new NotImplementedException();
        }


        [Authorize(Policy = "StandardUser")]
        [HttpPatch("User/Settings/Preferences")]
        public async Task<IActionResult> ChangePreferences([FromBody] UserPreferencesDto userPreferencesDto)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPost("User/Settings/Logout")]
        public async Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPatch("User/Settings/TwoFactorAuthActivation")]
        public async Task<IActionResult> ActivateTwoFactorAuth()
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPatch("User/Settings/TwoFactorAuthDeactivation")]
        public async Task<IActionResult> DeActivateTwoFactorAuth()
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPatch("User/Settings/PasswordResetViaEmail")]
        public async Task<IActionResult> EmailPasswordReset(string PasswordResetToken, string newEmail)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPatch("User/Settings/PasswordResetViaSms")]
        public async Task<IActionResult> SmsPasswordReset(string passwordResetToken, string newPassword)
        {
            throw new NotImplementedException();
        }


    }
}
