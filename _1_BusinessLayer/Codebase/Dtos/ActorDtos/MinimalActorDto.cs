using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Dtos.UserDtos
{
    public class MinimalActorDto
    {
        public string? ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public BotGrades? Grade { get; set; }
        public bool IsBot { get; set; }
    }
}
