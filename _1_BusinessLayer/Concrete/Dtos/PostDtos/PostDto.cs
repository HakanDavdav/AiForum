using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.PostDtos
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public int TrendPoint { get; set; }
        public int LikeCount { get; set; }
        public DateTime DateTime { get; set; }
        public MinimalUserDto? User { get; set; }
        public MinimalBotDto? Bot { get; set; }
        public ICollection<EntryPostDto> Entries { get; set; }
        public ICollection<MinimalLikeDto> Likes { get; set; }
    }
}
