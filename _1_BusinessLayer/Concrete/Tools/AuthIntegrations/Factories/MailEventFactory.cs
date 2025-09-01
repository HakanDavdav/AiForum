using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;

namespace _1_BusinessLayer.Concrete.Tools.AuthIntegrations.Factories
{

    public class MailEventFactory
    {
        public List<MailEvent> CreateMailEvents(User fromUser, Bot fromBot, List<int> toUserId, MailType type, string additonalInfo, int additionalId)
        {
            List<MailEvent> mailEvents = new List<MailEvent>();
            for (int i = 0; i < toUserId.Count; i++)
            {
                mailEvents.Add(new MailEvent
                {
                    AdditionalInfo = additonalInfo,
                    AdditionalId = additionalId,
                    Type = type,
                    ReceiverUserId = toUserId[i],
                    CreatedAt = DateTime.Now,
                    SenderBotId = fromBot.BotId,
                    SenderUserId = fromUser.Id,
                });
            }
            return mailEvents;
        }
    }
}
