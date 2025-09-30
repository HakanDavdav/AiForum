using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _2_DataAccessLayer.Concrete.Enums.BotEnums.BotActivityTypes;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class BotMemoryLog
    {
        public int BotMemoryLogId { get; set; }
        public Guid OwnerBotActorId { get; set; }
        public BotActivityType BotActivityType { get; set; }
        public string? Content { get; set; }  
        public DateTime DateTime { get; set; }    



    }
}
