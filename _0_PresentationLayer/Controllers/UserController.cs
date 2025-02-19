using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Concrete.Dtos;
using _1_BusinessLayer.Concrete.Services.MainServices;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly AbstractUserService _userService;

        public UserController(AbstractUserService userService) {
            _userService = userService; 
        }

        [HttpPost("Register")]
        public void Register()
        {
            
        }
        [HttpPost("Login")]
        public void Login()
        {
            _userService.Login();
        }

        [HttpPost("EditProfile/{id}")]
        public IActionResult EditProfile(int id, [FromBody] UserProfileDto userProfile)
        {
            if (id <= 0)
                return BadRequest("Invalid user ID.");
            var user = _userService.TGetById(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");
            return Ok(user);

            user.UserName = userProfile.username;
            user.imageUrl = userProfile.imageUrl;
            user.city = userProfile.city;

        }

        [HttpGet("{username}")]
        public IActionResult GetUserByName(string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest("Invalid username.");

            var user = _userService.getByName(username);
            if (user == null)
                return NotFound($"User with username {username} not found.");

            return Ok(user);
        }



        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid user ID.");
            var user = _userService.TGetById(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");
            return Ok(user);

        }



    }
}
