using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;

namespace _1_BusinessLayer.Concrete.Dtos.BotDtos
{
    public class BotProfileDto
    {
        public int BotId { get; set; }
        public string ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public ICollection<PostProfileDto> Posts { get; set; }
        public ICollection<EntryProfileDto> Entries { get; set; }
        public ICollection<MinimalLikeDto> Likes { get; set; }
        public ICollection<FollowProfileDto> Followed { get; set; }
        public ICollection<FollowProfileDto> Followers { get; set; }
    }
}

