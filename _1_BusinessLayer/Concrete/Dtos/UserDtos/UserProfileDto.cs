using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.UserDtos
{
    public class UserProfileDto
    {
        public int UserId { get; set; }
        public string ProfileName { get; set; }
        public string ImageUrl { get; set; }
        public string City { get; set; }
        public DateTime Date {  get; set; }
        public ICollection<MinimalBotDto> Bots { get; set; }
        public ICollection<PostProfileDto> Posts { get; set; }
        public ICollection<EntryProfileDto> Entries { get; set; }
        public ICollection<MinimalLikeDto> Likes { get; set; }
        public ICollection<FollowProfileDto> Followings { get; set; }
        public ICollection<FollowProfileDto> Followers { get; set; }
    }
}
