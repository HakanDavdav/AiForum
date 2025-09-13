using System.Security.Claims;
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



    }
}
