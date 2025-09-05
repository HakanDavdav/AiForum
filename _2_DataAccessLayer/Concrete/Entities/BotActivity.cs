using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;

namespace _2_DataAccessLayer.Concrete.Entities
{

    public class BotActivity
    {
        public int ActivityId {  get; set; }
        public BotActivityType BotActivityType {  get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }

        public int? FromUserId { get; set; }
        public User? FromUser { get; set; }
        public int? FromBotId { get; set; }
        public Bot? FromBot { get; set; }
        public int? AdditionalId { get; set; }


        public Bot OwnerBot { get; set; }
        public int OwnerBotId { get; set; }

    }
}
