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
            PostLike,
            EntryLike,
            CreatingEntry,
            CreatingPost,
            Message,
            BotActivity,
            GainedFollower,
            NewEntryForPost
        }
    }
}
