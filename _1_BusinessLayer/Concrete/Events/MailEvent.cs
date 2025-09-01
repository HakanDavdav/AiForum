using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using static _2_DataAccessLayer.Concrete.Enums.MailTypes;

namespace _1_BusinessLayer.Concrete.Events
{

    public class MailEvent
    {

        public int SenderUserId { get; set; }
        public int SenderBotId { get; set; }
        public int ReceiverUserId { get; set; }
        public string AdditionalInfo { get; set; }
        public MailType Type { get; set; }
        public int AdditionalId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
