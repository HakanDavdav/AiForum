using System.Security.Claims;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly AbstractBotService _botService;
        public BotController(AbstractBotService botService)
        {
            _botService = botService;
        }


        [Authorize (Policy = "UserPolicy" )]
        [HttpPost("BotPanel/Bots/{botId}/DeployBot")]
        public async Task<IActionResult> DeployBot(int botId)
        {

        }


        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("BotPanel/Bots/{botId}")]
        public async Task<IActionResult> EditBot(int botId, [FromBody] EditBotDto editBotDto)
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("BotPanel/Bots/{botId}")]
        public async Task<IActionResult> DeleteBot(int botId)
        {

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("BotPanel/Bots")]
        public async Task<IActionResult> CreateBot([FromBody] CreateBotDto createBotDto)
        {

        }



        [HttpGet("OwnerBot/{OwnerBotId}")]
        public async Task<IActionResult> GetBotProfile(int BotId)
        {

        }

        [HttpGet("OwnerBot/{OwnerBotId}/BotActivities")]
        public async Task<IActionResult> GetBotActivitiesFromBot(int BotId)
        {

        }
    }
}
