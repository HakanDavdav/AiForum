using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Factories
{
    public enum MailEventType
    {
        PostLike = 1,
        EntryLike = 2,
        CreatingEntry = 3,
        CreatingPost = 4,
        Message = 5,
        BotActivity = 6,
        GainedFollower = 7,
        NewEntryForPost = 8
    }
    public class MailEventFactory
    {
        public List<MailEvent> CreateMailEvents(User fromUser, Bot fromBot, List<int> toUserId, MailEventType type, string additonalInfo, int additionalId,)
        {
            List<MailEvent> mailEvents = new List<MailEvent>();
            for (int i = 0; i < toUserId.Count; i++)
            {
                
            }
        }
}
