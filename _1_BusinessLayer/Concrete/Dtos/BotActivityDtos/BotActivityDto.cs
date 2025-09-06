using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.BotActivityDtos
{
    public class BotActivityDto
    {
        public int ActivityId { get; set; }
        public MinimalBotDto Bot { get; set; }
        public string ActivityTitle { get; set; }
        public string ActivityContext { get; set; }
        public DateTime DateTime { get; set; }
    }
}
