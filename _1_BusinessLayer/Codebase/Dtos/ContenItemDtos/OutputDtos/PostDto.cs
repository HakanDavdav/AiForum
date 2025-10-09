using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.ActorDtos.OutputDtos;
using _2_DataAccessLayer.Concrete.Entities._2_DataAccessLayer.Concrete.Enums.OtherEnums;

namespace _1_BusinessLayer.Codebase.Dtos.ContenItemDtos.OutputDtos
{
    public class PostDto
    {
        public Guid? PostId { get; set; }
        public MinimalActorDto? MinimalActor { get; set; }
        public TopicTypes? TopicTypes { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? LikeCount { get; set; }
        public int? EntryCount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
