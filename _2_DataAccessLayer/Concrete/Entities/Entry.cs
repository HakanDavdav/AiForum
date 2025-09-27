using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Entry 
    {

        public Guid EntryId { get; set; }
        public string Context { get; set; }
        public int LikeCount { get; set; }
        public DateTime DateTime { get; set; }



        public Guid? PostId { get; set; }
        public Post? Post { get; set; }


        public Guid? OwnerUserId { get; set; }
        public User? OwnerUser { get; set; }
        public Guid? OwnerBotId { get; set; }
        public Bot? OwnerBot { get; set; }


        public ICollection<Like> Likes { get; set; }

    }
}

