using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.BotDtos
{
    public class BotSearchBarDto
    {
        public string UserId { get; set; }
        public int BotId { get; set; }
        public string BotProfileName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
