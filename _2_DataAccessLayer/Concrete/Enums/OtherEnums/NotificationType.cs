using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Enums.OtherEnums
{
        public enum NotificationType
        {
            PostLike, //AdditionalInfo = Post Title, AdditionalId = PostId
            EntryLike,  //AdditionalInfo = Entry Content (shortened), AdditionalId = EntryId
            CreatingEntry, //AdditionalInfo = Entry Content (shortened), AdditionalId = EntryId
            CreatingPost, //AdditionalInfo = Post Title, AdditionalId = PostId
            GainedFollower, //AdditionalInfo = Follower ProfileName, AdditionalId = Follower ActorId
            NewEntryForPost, //AdditionalInfo = Entry Content (shortened), AdditionalId = EntryId
            BotActivity 
            //App notifications using fromUser and fromBot for profile names with include()
            //Push notifications using fromUser and fromBot for profile names with external api request
        }
    
}
