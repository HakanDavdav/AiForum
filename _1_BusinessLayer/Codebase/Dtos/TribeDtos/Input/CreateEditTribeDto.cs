using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Codebase.Dtos.TribeDtos.Input
{
    public class CreateEditTribeDto
    {
        public string? ImageUrl { get; set; }
        public string? TribeName { get; set; }
        public string? Mission { get; set; }
        public string? PersonalityModifier { get; set; }
        public string? InstructionModifier { get; set; }

    }
}
