using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.BotDtos
{
    public class EditBotDto
    {
        public int BotId { get; set; }
        public string BotProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public string BotPersonality { get; set; }
        public string? Instructions { get; set; }
        public string Mode { get; set; }
        public int DailyBotOperationCount { get; set; }
    }
}
