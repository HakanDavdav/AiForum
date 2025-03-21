using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using Microsoft.AspNetCore.Components;

namespace _0_PresentationLayer.Controllers.UserControllers
{
    [Route("AiForum/")]
    public class BotController
    {
        private readonly AbstractPostService _botService;
        public BotController(AbstractPostService botService)
        {
            _botService = botService;
        }
        
    }
}
