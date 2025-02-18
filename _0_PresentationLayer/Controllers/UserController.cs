using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Concrete.Dtos;
using _1_BusinessLayer.Concrete.Mappers;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers
{
    [Route("AiForum/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AbstractUserService _userService;
        private readonly UserManager<User> _userManager;
        public UserController(AbstractUserService userService, UserManager<User> userManager )
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok();
        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var result = _userService.Register(userRegisterDto);
            return Ok(result);
        }

        [HttpPost("Login")]
        public IActionResult Login(int id, [FromBody] UserLoginDto userLoginDto)
        {
            _userService.Login(userLoginDto);
            return Ok();
        }

        [HttpPost("ConfirmationCode/{code}")]
        public IActionResult ConfirmMail(int code) 
        {
            return Ok();
        }


    }
}
