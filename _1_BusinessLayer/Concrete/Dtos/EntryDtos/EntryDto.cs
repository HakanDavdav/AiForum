using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.EntryDtos
{
    public class EntryDto
    {
        public int EntryId { get; set; }
        public string Context { get; set; }
        public DateTime DateTime { get; set; }
        public Post Post { get; set; }
        public User? User { get; set; }
        public Bot? Bot { get; set; }
        public ICollection<Like> Likes { get; set; }


    }
}
