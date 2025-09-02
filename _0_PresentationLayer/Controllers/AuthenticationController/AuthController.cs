using System.Security.Claims;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.GuestControllers
{
    [Route("AiForum")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AbstractUserIdentityService _userIdentityService;
        private readonly AbstractUserService _userService;
        public AuthController(AbstractUserIdentityService userIdentityService, AbstractUserService userService)
        {
            _userIdentityService = userIdentityService;
            _userService = userService;
        }

       
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var result = await _userIdentityService.Register(userRegisterDto);
            return Ok(result);
        }

        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var result = await _userIdentityService.LoginDefault(userLoginDto);
            return Ok(result);
        }

        [HttpPost("CreateProfile")]
        public async Task<IActionResult> CreateProfile([FromBody] UserCreateProfileDto userCreateProfileDto)
        {
            var result = await _userService.CreateProfileAsync((int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)),userCreateProfileDto);
            return Ok(result);
        }


        [HttpPost("TwoFactorLogin")]
        public async Task<IActionResult> LoginTwoFactor([FromBody] UserLoginDto userLoginDto, string twoFactorToken, string provider)
        {
            var result = await _userIdentityService.LoginTwoFactor(userLoginDto,twoFactorToken,provider);
            return Ok(result);
        }


        [HttpPost("ConfirmEmailConfirmationToken")]
        public async Task<IActionResult> ConfirmEmail(string usernameEmailOrPhoneNumber, string confirmMailToken)
        {
            var result = await _userIdentityService.ConfirmEmailConfirmationToken(confirmMailToken, usernameEmailOrPhoneNumber);
            return Ok(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string usernameEmailOrPhoneNumber, string resetPasswordToken, string newPassword)
        {
            var result = await _userIdentityService.ConfirmPasswordResetToken(usernameEmailOrPhoneNumber, resetPasswordToken, newPassword);
            return Ok(result);
        }


        [HttpPost("ChooseProviderAndSendToken")]
        public async Task<IActionResult> ChooseProviderAndSendToken(string usernameEmailOrPhoneNumber, string provider, string operation)
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference BotActivityType.
            var result = await _userIdentityService.ChooseProviderAndSendToken(provider, operation, usernameEmailOrPhoneNumber);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference BotActivityType.
            return Ok(result);
        }

    }
}
