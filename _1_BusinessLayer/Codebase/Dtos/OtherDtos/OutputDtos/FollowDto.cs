using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.ActorDtos.OutputDtos;

namespace _1_BusinessLayer.Codebase.Dtos.OtherDtos.OutputDtos
{
    public class FollowDto
    {
        public DateTime? CreatedAt { get; set; }
        public MinimalActorDto? Follower { get; set; }
        public MinimalActorDto? Followed { get; set; }
    }
}
