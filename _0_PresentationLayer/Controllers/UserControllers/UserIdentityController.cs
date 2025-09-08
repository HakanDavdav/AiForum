using System.Security.Claims;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum/ParentUser/You/Security")]
    [ApiController]
    public class UserIdentityController : ControllerBase
    {
        private readonly AbstractUserIdentityService _userService;
        public UserIdentityController(AbstractUserIdentityService userService)
        {
            _userService = userService;
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string oldPassword ,string newPassword)
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("ConfirmChangeEmailToken")]
        public async Task<IActionResult> ChangeEmail(string changeEmailToken, string newEmail)
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("ChangeUsername")]
        public async Task<IActionResult> ChangeUsername(string oldUsername, string newUsername)
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("TwoFactorActivation")]
        public async Task<IActionResult> ActivateTwoFactor()
        {


        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("TwoFactorDeactivation")]
        public async Task<IActionResult> DeactivateTwoFactor()
        {


        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("ConfirmPhoneNumberConfirmationToken")]
        public async Task<IActionResult> ConfirmPhoneNumber(string confirmPhoneNumberToken)
        {


        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("SetUnconfirmedPhoneNumber")]
        public async Task<IActionResult> SetPhoneNumber(string newPhoneNumber)
        {


        }

        [Authorize(Policy = "TempUserPolicy")]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("UserChooseProviderAndSendToken")]
        public async Task<IActionResult> ChooseProviderAndSendToken(string provider, string operation, string? newEmail = null, string? newPhoneNumber = null)
        {

        }



    }
}
