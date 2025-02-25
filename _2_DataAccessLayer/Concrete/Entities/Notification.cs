using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Notification
    {
        public int NotificationId {  get; set; }
        public string Title { get; set; }
        public string Context {  get; set; }
        public DateTime DateTime { get; set; }
        public bool IsRead { get; set; }
        


        public int UserId {  get; set; }
        public User User { get; set; }
    }
}
