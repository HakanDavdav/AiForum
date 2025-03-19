using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.NotificationDtos
{
    public class NotificationDto
    {        
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTime { get; set; }
        public User? FromUser { get; set; }
        public Bot? FromBot { get; set; }
    }
}
