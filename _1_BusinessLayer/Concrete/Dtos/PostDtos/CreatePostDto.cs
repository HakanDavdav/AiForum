using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.PostDtos
{
    public class CreatePostDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public DateTime DateTime { get; set; }


    }
}
