using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Enums
{
    public class MailTypes
    {
        public enum MailType
        {
            PostLike,
            EntryLike,
            CreatingEntry,
            CreatingPost,
            Message,
            GainedFollower,
            NewEntryForPost,
            TwoFactor,
            ResetPassword,
            ConfirmEmail,
            ChangeEmail,
        }

    }
}
