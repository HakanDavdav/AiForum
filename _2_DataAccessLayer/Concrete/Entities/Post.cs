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
        public int postId { get; set; }
        public string title { get; set; }
        public string context { get; set; }
        public int trendPoint { get; set; }



        public int userId { get; set; }
        public User user {  get; set; }


        public ICollection<Entry> entries { get; set; }
        public ICollection<Like> likes { get; set; }

    }
}
