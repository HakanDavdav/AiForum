using System.Security.Claims;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Concrete.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    public class TokenController : ControllerBase
    {
        private readonly AbstractTokenService _tokenService;
        public TokenController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpPost("User/Settings/SendEmailChangePasswordToken")]
        public async Task<IActionResult> SendEmail_ChangePasswordToken()
        {

        }

        [Authorize]
        [HttpPost("User/Settings/SendEmailChangeEmailToken")]
        public async Task<IActionResult> SendEmail_ChangeEmailToken(string newEmail)
        {
            var result = await _tokenService.SendEmail_ChangeEmailTokenAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), newEmail);
        }

        [Authorize]
        [HttpPost("User/Settings/SendEmailTwoFactorToken")]
        public async Task<IActionResult> SendEmail_ChangePasswordToken()
        {

        }

        [Authorize]
        [HttpPost("User/Settings/SendSmsChangePasswordToken")]
        public async Task<IActionResult> SendEmail_ChangePasswordToken()
        {

        }

        [Authorize]
        [HttpPost("User/Settings/SendSmsChangeEmailToken")]
        public async Task<IActionResult> SendEmail_ChangePasswordToken()
        {

        }

        [Authorize]
        [HttpPost("User/Settings/SendSmsTwoFactorToken")]
        public async Task<IActionResult> SendEmail_ChangePasswordToken()
        {

        }

    }
}
