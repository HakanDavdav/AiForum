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


        [Authorize]
        [HttpPost("BotPanel/DeployBot/{botId}")]
        public async Task<IActionResult> DeployAllBots(int botId)
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


        [Authorize]
        [HttpPatch("BotPanel/EditBot/{botId}")]
        public async Task<IActionResult> EditBot(int botId, EditBotDto editBotDto)
        {
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

        [Authorize]
        [HttpPost("BotPanel/DeleteBot/{botId}")]
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

        [Authorize]
        [HttpPost("BotPanel/CreateBot")]
        public async Task<IActionResult> CreateBot(CreateBotDto createBotDto)
        {
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



        [HttpGet("{BotId}")]
        public async Task<IActionResult> GetBotProfile(int BotId)
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _botService.GetBotProfileAsync(BotId);
#pragma warning restore CS8604 // Possible null reference argument.
                return result.ResultWrapErrorCode();
            }
            catch (Exception e)
            {
                return e.ExceptionWrapErrorCode();
            }
        }

        [HttpGet("{BotId}")]
        public async Task<IActionResult> GetBotActivitiesFromBot(int BotId)
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var result = await _botService.GetBotProfileAsync(BotId);
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
