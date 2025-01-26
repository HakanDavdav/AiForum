using _1_BusinessLayer.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers
{
    [ApiController]
    [Route("AiForum")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserByID(id);
            return Ok(user);
        }
    }
}
