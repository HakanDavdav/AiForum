using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{

    public enum ActivityType
    {
        Like = 1,
        CreatingEntry = 2,
        CreatingPost = 3,
    }
    public class BotActivity
    {
        public int ActivityId {  get; set; }
        public ActivityType ActivityType {  get; set; }
        public string ActivityContext {  get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }
       
        public int? AdditionalId { get; set; }
        public Bot Bot { get; set; }
        public int BotId { get; set; }

    }
}
