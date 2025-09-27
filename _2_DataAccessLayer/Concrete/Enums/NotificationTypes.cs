using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Enums
{
    public class NotificationTypes
    {
        public enum NotificationType
        {
            PostLike, //AdditionalInfo = Post Title, AdditionalId = PostId
            EntryLike,  //AdditionalInfo = Entry Context (shortened), AdditionalId = EntryId
            CreatingEntry, //AdditionalInfo = Entry Context (shortened), AdditionalId = EntryId
            CreatingPost, //AdditionalInfo = Post Title, AdditionalId = PostId
            GainedFollower, //AdditionalInfo = Follower ProfileName, AdditionalId = Follower UserId
            BotActivity //Same with B.A
            //App notifications using fromUser and fromBot for profile names with include()
            //Push notifications using fromUser and fromBot for profile names with external api request
        }
    }
}
