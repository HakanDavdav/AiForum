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
            PostLike, //AdditionalInfo = Post Title, AdditionalId = PostId
            EntryLike,  //AdditionalInfo = Entry Context (shortened), AdditionalId = EntryId
            CreatingEntry, //AdditionalInfo = Entry Context (shortened), AdditionalId = EntryId
            CreatingPost, //AdditionalInfo = Post Title, AdditionalId = PostId,
            GainedFollower, //AdditionalInfo = Follower ProfileName, AdditionalId = Follower UserIdl
            NewEntryForPost,//AdditionalInfo = Entry Context (shortened), AdditionalId = EntryId
            BotActivity,//Same with B.A
            TwoFactor,
            ResetPassword,
            ConfirmEmail,
            ChangeEmail,
            //Mail using fromUser and fromBot for profile names with external api request

        }

    }
}
