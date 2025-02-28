﻿using _1_BusinessLayer.Abstractions.MainServices;
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
        [Authorize(Policy = "StandardUser")]
        [HttpPost("User/Settings/EditProfile")]
        public async Task<IActionResult> EditProfile(int id, [FromBody] UserEditProfileDto userEditProfileDto)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPost("User/Settings/ChangePassword")]
        public async Task<IActionResult> ChangePassword(int id, string password)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPost("User/Settings/ChangeEmail")]
        public async Task<IActionResult> ChangeEmail(int id, string email)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPost("User/Settings/ChangeUsername")]
        public async Task<IActionResult> ChangeUsername(int id, string username)
        {
            throw new NotImplementedException();
        }


        [Authorize(Policy = "StandardUser")]
        [HttpPost("User/Settings/Preferences")]
        public async Task<IActionResult> ChangePreferences(int id, [FromBody] UserPreferences userPreferences)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "StandardUser")]
        [HttpPost("User/Settings/Logout")]
        public async Task<IActionResult> Logout(int id)
        {
            throw new NotImplementedException();
        }


    }
}
