using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.ActorDtos.OutputDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Dtos.OtherDtos.OutputDtos
{
    public class TribeDto
    {
        public Guid TribeId { get; set; }
        public int TribePoint { get; set; }
        public string? ImageUrl { get; set; }
        public string? TribeName { get; set; }
        public string? Mission { get; set; }
        public int MemberCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TribeRivalry>? TribeRivalries { get; set; }
        public List<MinimalActorDto>? Members { get; set; }
    }
}
