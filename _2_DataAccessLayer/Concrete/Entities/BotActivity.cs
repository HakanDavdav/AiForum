using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _2_DataAccessLayer.Concrete.Enums.BotEnums.BotActivityTypes;

namespace _2_DataAccessLayer.Concrete.Entities
{

    public class BotActivity
    {
        public Guid BotActivityId {  get; set; }
        public BotActivityType BotActivityType {  get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }


        public Guid? AdditionalId { get; set; }
        public string? AdditionalInfo { get; set; }

        public Actor FromActor { get; set; }
        public Guid FromActorId { get; set; }
        public Bot OwnerBotActor { get; set; }
        public Guid? OwnerBotActorId { get; set; }

    }
}
