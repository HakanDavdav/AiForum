using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities._2_DataAccessLayer.Concrete.Enums.OtherEnums;

namespace _1_BusinessLayer.Codebase.Dtos.ContenItemDtos.InputDtos
{
    public class CreateEditPostDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public TopicTypes? TopicTypes { get; set; }

    }
}

