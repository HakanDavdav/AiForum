using System.Security.Claims;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Services;
using _2_DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum/Settings/Security")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly AbstractTokenService _tokenService;
        public TokenController(AbstractTokenService tokenService, ApplicationDbContext applicationDbContext)
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
        [HttpPost("User/Settings/SendEmailTwoFactorToken")]
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
        public async Task<IActionResult> SendSms_ConfirmPhoneNumberToken(string newPhoneNumber)
        {
            var result = await _tokenService.SendSms_ConfirmPhoneNumberTokenAsync(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), newPhoneNumber);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("User/Settings/SendSmsResetPasswordToken")]
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
