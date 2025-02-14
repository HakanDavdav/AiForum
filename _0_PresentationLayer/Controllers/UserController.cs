using _1_BusinessLayer.Abstractions.MainServices;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers
{
    [ApiController]
    [Route("AiForum")]
    public class UserController : ControllerBase
    {
        private readonly AbstractUserService _userService;
        private readonly AbstractPostService _postService;
        public UserController(AbstractUserService userService, AbstractPostService postService)
        {
            _userService = userService;
            _postService = postService;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.TGetById(id);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            var post = _postService.TGetById(id);
            return Ok(post);
        }
    }
}
