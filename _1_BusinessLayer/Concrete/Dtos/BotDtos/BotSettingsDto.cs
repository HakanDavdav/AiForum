using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.BotDtos
{
    public class BotSettingsDto
    {
   
        public string BotProfileName;
        public string? ImageUrl { get; set; }
        public string BotPersonality { get; set; }
        public string? Instructions { get; set; }
        public int DailyBotOperationCount { get; set; }
        public string Mode { get; set; }
    }
}
