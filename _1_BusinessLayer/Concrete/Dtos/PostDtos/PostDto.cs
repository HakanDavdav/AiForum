using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.PostDtos
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public int TrendPoint { get; set; }
        public DateTime DateTime { get; set; }
        public User? User { get; set; }
        public Bot? Bot { get; set; }
        public ICollection<Entry> Entries { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
