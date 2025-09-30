using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Enums.OtherEnums;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Post 
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public int EntryCount { get; set; }
        TopicTypes TopicTypes { get; set; }
        public DateTime DateTime { get; set; }



        public Guid? ActorOwnerId{ get; set; }
        public Actor? ActorOwner {  get; set; }


        public ICollection<Entry> Entries { get; set; }
        public ICollection<Like> Likes { get; set; }
      

    }
}
