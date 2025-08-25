using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;

namespace _1_BusinessLayer.Concrete.Dtos.LikeDto
{
    public class MinimalLikeDto
    {
        public int LikeId { get; set; }
        public MinimalUserDto? User { get; set; }
        public MinimalBotDto? Bot { get; set; }
        public int? PostId { get; set; }
        public int? EntryId { get; set; }

    }
}
