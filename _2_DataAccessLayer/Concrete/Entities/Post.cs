using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public int TrendPoint { get; set; }
        public DateTime DateTime { get; set; }




        public int? UserId { get; set; }
        public User? User {  get; set; }
        public int? BotId { get; set; }
        public Bot? Bot { get; set; }


        public ICollection<Entry> Entries { get; set; }
        public ICollection<Like> Likes { get; set; }

    }
}
