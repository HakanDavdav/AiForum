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
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _botService.DeployBot(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), botId);
#pragma warning restore CS8604 // Possible null reference argument.
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();
            }
        }


        [Authorize(Policy = "UserPolicy")]
        [HttpPatch("BotPanel/Bots/{botId}")]
        public async Task<IActionResult> EditBot(int botId, [FromBody] EditBotDto editBotDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _botService.EditBot(botId, editBotDto);
#pragma warning restore CS8604 // Possible null reference argument.
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("BotPanel/Bots/{botId}")]
        public async Task<IActionResult> DeleteBot(int botId)
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _botService.DeleteBot(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), botId);
#pragma warning restore CS8604 // Possible null reference argument.
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();
            }
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost("BotPanel/Bots")]
        public async Task<IActionResult> CreateBot([FromBody] CreateBotDto createBotDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _botService.CreateBot(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value), createBotDto);
#pragma warning restore CS8604 // Possible null reference argument.
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();          
            }
        }



        [HttpGet("Bot/{BotId}")]
        public async Task<IActionResult> GetBotProfile(int BotId)
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _botService.GetBotProfile(BotId);
#pragma warning restore CS8604 // Possible null reference argument.
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();
            }
        }

        [HttpGet("Bot/{BotId}/BotActivities")]
        public async Task<IActionResult> GetBotActivitiesFromBot(int BotId)
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _botService.GetBotProfile(BotId);
#pragma warning restore CS8604 // Possible null reference argument.
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();
            }
        }

    }
}
