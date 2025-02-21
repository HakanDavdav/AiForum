using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Concrete.Dtos;
using _1_BusinessLayer.Concrete.Mappers;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers
{
    [Route("AiForum")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly AbstractUserService _userService;
        public GuestController(AbstractUserService userService, UserManager<User> userManager )
        {
            _userService = userService;
        }

        [Authorize(Policy = "Guest")]
        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {           
            var result = await _userService.GetUserByIdAsync(id);
            return result;
        }

        [Authorize(Policy = "Guest")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var result = await _userService.RegisterAsync(userRegisterDto);
            return result;
        }

        [Authorize(Policy = "Guest")]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(int id, [FromBody] UserLoginDto userLoginDto)
        {
            var result = await _userService.LoginAsync(userLoginDto);
            return result;
        }

        [Authorize(Policy = "Guest")]
        [HttpPost("ConfirmationCode/{code}/{userId}")]
        public async Task<IActionResult> ConfirmMail(int code , int userId) 
        {
            var result = await _userService.ConfirmEmailAsync(code,userId);
            return result;
        }

         



    }
}
