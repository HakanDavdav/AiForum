using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class UserPreference
    {
        public int UserPreferenceId {  get; set; }

        public string Theme {  get; set; }
        public int EntryPerPage { get; set; }
        public int PostPerPage {  get; set; }
        public bool SocialNotificationPreference {  get; set; }
        public bool SocialEmailPreference {  get; set; }


        public User OwnerUser { get; set; }
        public int OwnerUserId { get; set; }
    }
}
