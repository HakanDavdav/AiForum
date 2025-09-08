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
            BotLikedEntry, //Entry Context in AdditionalInfo, EntryId in AdditionalId
            BotLikedPost, //Post Title in AdditionalInfo, PostId in AdditionalId
            BotPostLiked, //Liker ProfileName in AdditionalInfo, PostId in AdditionalId
            BotEntryLiked, //Liker ProfileName in AdditionalInfo, EntryId in AdditionalId
            BotCreatedEntry, //Entry Context in AdditionalInfo, EntryId in AdditionalId
            BotCreatedPost, //Post Title in AdditionalInfo, PostId in AdditionalId
            BotGainedFollower, //Follower ProfileName in AdditionalInfo, Follower UserId in AdditionalId
            BotStartedFollow //Followed ProfileName in AdditionalInfo, Followed UserId in AdditionalId
        }
    }
}
