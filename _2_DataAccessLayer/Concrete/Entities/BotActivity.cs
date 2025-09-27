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
        public Guid ActivityId {  get; set; }
        public BotActivityType BotActivityType {  get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }


        public Guid? AdditionalId { get; set; }
        public string? AdditionalInfo { get; set; }
        public Guid? FromUserId { get; set; }
        public User? FromUser { get; set; }
        public Guid? FromBotId { get; set; }
        public Bot? FromBot { get; set; }

        public Bot? OwnerBot { get; set; }
        public Guid? OwnerBotId { get; set; }

    }
}
