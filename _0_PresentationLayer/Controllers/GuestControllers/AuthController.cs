using System.Security.Claims;
using _1_BusinessLayer.Abstractions.AbstractServices;
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
        private readonly AbstractUserIdentityService _userService;
        public AuthController(AbstractUserIdentityService userService, UserManager<User> userManager)
        {
            _userService = userService;
        }

       
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var result = await _userService.Register(userRegisterDto);
            return Ok(result);
        }

        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var result = await _userService.LoginDefault(userLoginDto);          
            return Ok(result);
        }


        [HttpPost("TwoFactorLogin")]
        public async Task<IActionResult> LoginTwoFactor([FromBody] UserLoginDto userLoginDto, string twoFactorToken, string provider)
        {
            var result = await _userService.LoginTwoFactor(userLoginDto,twoFactorToken,provider);
            return Ok(result);
        }


        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string usernameEmailOrPhoneNumber, string confirmMailToken)
        {
            var result = await _userService.ConfirmEmail(confirmMailToken, usernameEmailOrPhoneNumber);
            return Ok(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string usernameEmailOrPhoneNumber, string resetPasswordToken, string newPassword)
        {
            var result = await _userService.PasswordReset(usernameEmailOrPhoneNumber, resetPasswordToken, newPassword);
            return Ok(result);
        }


        [HttpPost("ChooseProviderAndSendToken")]
        public async Task<IActionResult> ChooseProviderAndSendToken(string usernameEmailOrPhoneNumber, string provider, string operation)
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await _userService.ChooseProviderAndSendToken(provider, operation, usernameEmailOrPhoneNumber);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            return Ok(result);
        }

    }
}
