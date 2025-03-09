using _1_BusinessLayer.Abstractions.AbstractServices;
using Microsoft.AspNetCore.Components;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum/")]
    public class BotController
    {
        private readonly AbstractBotService _botService;
        public BotController(AbstractBotService botService)
        {
            _botService = botService;
        }
        
    }
}
