using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{

    public class BotActivity
    {
        public Guid BotActivityId { get; set; }
        public Guid? BotId { get; set; }
        public Bot? Bot { get; set; }
        public Guid? AdditionalId { get; set; }
        public IdTypes? AdditionalIdType { get; set; }
        public string? Message { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
