using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Enums;
using static _2_DataAccessLayer.Concrete.Enums.ActorTypes;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Entry 
    {

        public Guid EntryId { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public DateTime DateTime { get; set; }



        public Guid? PostId { get; set; }
        public Post? Post { get; set; }


        public Guid? ActorOwnerId { get; set; }
        public Actor? ActorOwner { get; set; }


        public ICollection<Like> Likes { get; set; }

    }
}

