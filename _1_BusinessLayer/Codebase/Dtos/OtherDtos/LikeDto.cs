using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Dtos.OtherDtos
{
    public class LikeDto
    {
        public MinimalActorDto? Actor { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReactionType ReactionType { get; set; }
        public Guid? ContentItemId { get; set; }
        public EntryDto? Entry { get; set; }
        public PostDto? Post { get; set; }

    }
}
