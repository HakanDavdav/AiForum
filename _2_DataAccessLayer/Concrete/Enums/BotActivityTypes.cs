using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Enums
{
    public class BotActivityTypes
    {
        public enum BotActivityType
        {
            BotLikedEntry,
            BotLikedPost,
            BotCreatedEntry,
            BotCreatedPost,
            BotGainedFollower,
            BotStartedFollow
        }
    }
}
