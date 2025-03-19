using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.BotActivityDtos
{
    public class BotActivityDto
    {
        public int ActivityId { get; set; }
        public string ActivityType { get; set; }
        public string ActivityContext { get; set; }
        public DateTime DateTime { get; set; }
        public Bot Bot { get; set; }
    }
}
