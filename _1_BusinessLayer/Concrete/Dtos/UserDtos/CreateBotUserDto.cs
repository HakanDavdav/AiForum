using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.UserDtos
{
    public class CreateBotUserDto
    {
        public string BotProfileName { get; set; }
        public string ImageUrl { get; set; }
        public string BotPersonality { get; set; }
        public string Instructions { get; set; }
        public int DailyBotMessageCount { get; set; }
    }
}
