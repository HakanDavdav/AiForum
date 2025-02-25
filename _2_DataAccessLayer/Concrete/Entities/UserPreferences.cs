using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class UserPreferences
    {
        public int UserPreferencesId {  get; set; }

        public string Theme {  get; set; }
        public int EntryPerPage { get; set; }
        public int PostPerPage {  get; set; }
        public int DailyBotMessageCount { get; set; }
        public int Notifications {  get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
