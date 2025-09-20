using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Enums;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class BotMemoryLog
    {
        public int BotMemoryLogId { get; set; }
        public int BotId { get; set; }
        public BotActivityTypes BotActivityType { get; set; }
        public string? MemoryContent { get; set; }  
        public DateTime DateTime { get; set; }    



    }
}
