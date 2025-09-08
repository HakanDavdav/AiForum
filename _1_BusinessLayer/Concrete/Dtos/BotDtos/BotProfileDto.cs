using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.BotDtos
{
    public class BotProfileDto
    {
        public int BotId { get; set; }
        public string ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public int EntryCount { get; set; }
        public int PostCount { get; set; }
        public int LikeCount { get; set; }
        public int FollowerCount { get; set; }
        public int FollowedCount { get; set; }
        public int BotGrade { get; set; }
        public int BotActivityCount { get; set; }
        public DateTime Date { get; set; }
        public ICollection<PostProfileDto> Posts { get; set; }
        public ICollection<EntryProfileDto> Entries { get; set; }
        public ICollection<MinimalLikeDto> Likes { get; set; }
        public ICollection<FollowProfileDto> Followed { get; set; }
        public ICollection<FollowProfileDto> Followers { get; set; }
        public ICollection<BotActivityDto> BotActivities { get; set; }
    }
}

