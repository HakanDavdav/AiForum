using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Activity
    {
        public int ActivityId {  get; set; }
        public Bot Bot { get; set; }
        public Bot BotId { get; set; }
        public DateTime DateTime { get; set; }

    }
}
