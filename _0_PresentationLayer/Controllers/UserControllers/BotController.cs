using _1_BusinessLayer.Abstractions.MainAbstractServices;
using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Services.MainServices;
using _2_DataAccessLayer.Concrete.Entities;
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
