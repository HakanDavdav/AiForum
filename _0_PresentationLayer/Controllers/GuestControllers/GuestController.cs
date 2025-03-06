﻿using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.GuestControllers
{
    [Route("AiForum")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly AbstractUserService _userService;
        public GuestController(AbstractUserService userService, UserManager<User> userManager)
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

        [HttpPost("ChooseProvider")]
        public async Task<IActionResult> ChooseProvider([FromBody] UserLoginDto userLoginDto, string provider)
        {
            var result = await _userService.ChooseProvider(provider, provider);
            return Ok(result);
        }

        [HttpPost("TwoFactorLogin")]
        public async Task<IActionResult> LoginTwoFactor([FromBody] UserLoginDto userLoginDto, string twoFactorToken, string provider)
        {
            var result = await _userService.LoginTwoFactor(userLoginDto,twoFactorToken,provider);
            return Ok(result);
        }


        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] UserLoginDto userLoginDto, string confirmMailToken)
        {
            var result = await _userService.ConfirmEmail(userLoginDto,confirmMailToken);
            return Ok(result);
        }
       

    }
}
