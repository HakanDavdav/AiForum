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
        [HttpPost("User/Settings/SendEmailChangeEmailToken")]
        public async Task<IActionResult> SendEmail_ChangeEmailToken(string newEmail)
        {
            var result = await _tokenService.SendEmail_ChangeEmailTokenAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), newEmail);
            return Ok(result);
        }


        [Authorize]
        [HttpPost("User/Settings/SendTwoFactorToken")]
        public async Task<IActionResult> SendEmail_TwoFactorToken()
        {
            var result = await _tokenService.SendEmail_TwoFactorTokenAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            return Ok(result);
        }

        [Authorize]
        [HttpPost("User/Settings/SendEmailResetPasswordToken")]
        public async Task<IActionResult> SendEmail_ResetPasswordToken()
        {
            var result = await _tokenService.SendEmail_ResetPasswordTokenAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            return Ok(result);
        }




        [Authorize]
        [HttpPost("User/Settings/SendSmsConfirmPhoneNumberToken")]
        public async Task<IActionResult> SendSms_ConfirmPhoneNumberToken()
        {
            var result = await _tokenService.SendSms_ConfirmPhoneNumberTokenAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            return Ok(result);
        }

        [Authorize]
        [HttpPost("User/Settings/SendSmsResetPassowrdToken")]
        public async Task<IActionResult> SendSms_ResetPasswordToken()
        {
            var result = await _tokenService.SendSms_ResetPasswordTokenAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            return Ok(result);
        }


        [Authorize]
        [HttpPost("User/Settings/SendSmsTwoFactorToken")]
        public async Task<IActionResult> SendSms_TwoFactorToken()
        {
            var result = await _tokenService.SendSms_TwoFactorTokenAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            return Ok(result);
        }

        }
}
