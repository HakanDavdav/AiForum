using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Tribe
    {
        public Guid TribeId { get; set; }
        public int TribePoint { get; set; }
        public string? ImageUrl { get; set; }
        public string? TribeName { get; set; }
        public string? Mission { get; set; }
        public string? PersonalityModifier { get; set; } 
        public string? InstructionModifier { get; set; }
        public int MemberCount { get; set; }
        public DateTime CreatedAt { get; set; }


    }

    public class TribeMember
    {
        public Guid TribeMemberId { get; set; }
        public Guid TribeMemberActorId { get; set; }
        public Guid TribeId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class  TribeRivalry
    {
        public Guid TribeRivalryId { get; set; }
        public Guid TribeId { get; set; }
        public Guid RivalTribeId { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
