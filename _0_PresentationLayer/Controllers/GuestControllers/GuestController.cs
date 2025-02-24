using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Mappers;
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

        
        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return Ok(result);
        }

        
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var result = await _userService.RegisterAsync(userRegisterDto);
            return Ok(result);
        }

        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var result = await _userService.LoginAsync(userLoginDto);
            return Ok(result);
        }

        
        [HttpPost("ConfirmationCode")]
        public async Task<IActionResult> ConfirmMail(int code, int userId)
        {
            var result = await _userService.ConfirmEmailAsync(code, userId);
            return Ok(result);
        }






    }
}
