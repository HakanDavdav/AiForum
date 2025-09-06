using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.NotificationDtos
{
    public class NotificationDto
    {        
        public int NotificationId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationContext { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }
        public MinimalUserDto OwnerUser { get; set; }
        public MinimalUserDto? FromUser { get; set; }
        public MinimalBotDto? FromBot { get; set; }
    }
}
