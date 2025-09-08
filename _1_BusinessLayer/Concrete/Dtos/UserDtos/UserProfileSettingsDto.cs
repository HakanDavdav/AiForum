using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.UserDtos
{
    public class UserProfileSettingsDto
    {
        public int UserId { get; set; }
        public string ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public string? City { get; set; }
        public int followerCount { get; set; }
        public int followedCount { get; set; }
        public int EntryCount { get; set; }
        public int PostCount { get; set; }
        public int LikeCount { get; set; }
        public DateTime Date { get; set; }
        public ICollection<BotSettingsDto> Bots { get; set; }
        public UserPreference UserPreference { get; set; }
    }
}
