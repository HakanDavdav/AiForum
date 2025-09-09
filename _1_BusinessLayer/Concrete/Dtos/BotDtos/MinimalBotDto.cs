using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;

namespace _1_BusinessLayer.Concrete.Dtos.BotDtos
{
    public class MinimalBotDto
    {
        public int BotId {  get; set; }
        public string? ImageUrl {  get; set; }
        public string ProfileName { get; set; }
        public List<MinimalBotDto>? ChildBots { get; set; }
    }
}
