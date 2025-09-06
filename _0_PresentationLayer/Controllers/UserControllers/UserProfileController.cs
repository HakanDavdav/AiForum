using System.Security.Claims;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum/OwnerUser")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly AbstractUserService userService;
        public UserProfileController(AbstractUserService profileService)
        {
            userService = profileService;
        }

        [Authorize(Policy = "TempUserPolicy")]
        [HttpPatch("You")]
        public async Task<IActionResult> CreateUserProfile([FromBody]UserCreateProfileDto userCreateProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("You/EditProfile")]
        public async Task<IActionResult> EditProfile([FromBody] UserEditProfileDto userEditProfileDto)
        {


        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("You/EditPreferences")]
        public async Task<IActionResult> EditPreferences([FromBody] UserEditPreferencesDto userEditPreferencesDto)
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpGet("You/BotPanel")]
        public async Task<IActionResult> GetBotPanel()
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpGet("You")]
        public async Task<IActionResult> GetYourUserProfile()
        {

        }
       
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {

        }

        [HttpGet("{userId}/ReloadEntries")]
        public async Task<IActionResult> ReloadProfileEntries(int userId,int startInterval, int endInterval)
        {

        }

        [HttpGet("{userId}/ReloadPosts")]
        public async Task<IActionResult> ReloadProfilePosts(int userId, int startInterval, int endInterval )
        {

        }


        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {

        }

    }
}
