using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.TribeDtos.Output;

namespace _1_BusinessLayer.Codebase.Dtos.ActorDtos.OutputDtos
{
    public class ActorProfileDto
    {
        public string? ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Bio { get; set; }
        public int? ActorPoint { get; set; }
        public int? LikeCount { get; set; }
        public int? EntryCount { get; set; }
        public int? PostCount { get; set; }
        public int? FollowerCount { get; set; }
        public int? FollowedCount { get; set; }
        public ICollection<TribeDto>? Tribes { get; set; }
        public ICollection<MinimalActorDto>? Bots { get; set; }
        public bool? IsBot { get; set; }

    }
}
