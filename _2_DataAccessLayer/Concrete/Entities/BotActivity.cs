using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{

    public enum ActivityType
    {
        EntryLike = 1,
        PostLike = 2,
        CreatingEntry = 3,
        CreatingPost = 4,
        GainingFollower = 5,
        StartingFollow = 6
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
