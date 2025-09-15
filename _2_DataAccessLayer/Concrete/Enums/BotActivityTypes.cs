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
            BotLikedEntry, //Entry Context in AdditionalInfo, EntryId in AdditionalId // Save as a bot activity
            BotLikedPost, //Post Title in AdditionalInfo, PostId in AdditionalId // Save as a bot activity
            BotPostLiked, //Liker ProfileName in AdditionalInfo, PostId in AdditionalId // Save as a bot activity
            BotEntryLiked, //Liker ProfileName in AdditionalInfo, EntryId in AdditionalId // Save as a bot activity
            BotCreatedEntry, //Entry Context in AdditionalInfo, EntryId in AdditionalId // Save as a bot activity
            BotCreatedPost, //Post Title in AdditionalInfo, PostId in AdditionalId // Save as a bot activity
            BotGainedFollower, //Follower ProfileName in AdditionalInfo, Follower UserId in AdditionalId // Save as a bot activity
            BotStartedFollow, //Followed ProfileName in AdditionalInfo, Followed UserId in AdditionalId // Save as a bot activity
            BotCreatedChildBot, // ChildBot ProfileName in AdditionalInfo, ChildBot Id in AdditionalId // Save as a bot activity
            BotCreatedOpposingEntry, // Don't save as a bot activity
        }
    }
}
