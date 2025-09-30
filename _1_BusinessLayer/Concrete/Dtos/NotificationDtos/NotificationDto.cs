using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums.OtherEnums;
using static _2_DataAccessLayer.Concrete.Enums.NotificationTypes;

namespace _1_BusinessLayer.Concrete.Dtos.NotificationDtos
{
    public class NotificationDto
    {        
        public int NotificationId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationContext { get; set; }
        public NotificationType NotificationType { get; set; }
        public int? AdditionalId { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }

    }
}
