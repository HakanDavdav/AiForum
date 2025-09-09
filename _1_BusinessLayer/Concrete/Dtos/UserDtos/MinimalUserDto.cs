using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;

namespace _1_BusinessLayer.Concrete.Dtos.UserDtos
{
    public class MinimalUserDto
    {
        public int UserId { get; set; }
        public string ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public List<MinimalBotDto>? Bots { get; set; }
    }
}
