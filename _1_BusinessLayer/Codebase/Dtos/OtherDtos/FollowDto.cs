using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.UserDtos;

namespace _1_BusinessLayer.Codebase.Dtos.OtherDtos
{
    public class FollowDto
    {
        public MinimalActorDto? Follower { get; set; }
        public MinimalActorDto? Followed { get; set; }
    }
}
