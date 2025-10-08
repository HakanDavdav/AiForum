using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Entities._2_DataAccessLayer.Concrete.Enums.OtherEnums;

namespace _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos
{
    public class UserProfileCreateEditDto
    {
        public Guid? UserId { get; set; }
        public string? ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Bio { get; set; }
        public TopicTypes? Interests { get; set; }
        public ThemeOptions Theme { get; set; }
        public int EntryPerPage { get; set; }
        public int PostPerPage { get; set; }
        public bool SocialNotificationPreference { get; set; }
        public bool SocialEmailPreference { get; set; }


    }
}
