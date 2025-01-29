using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Like
    {
        public int likeID {  get; set; }


        public int? postId { get; set; }
        public Post post { get; set; }


        public int? entryId { get; set; }
        public Entry entry { get; set; }


        public int? userId { get; set; }
        public User user { get; set; }
    }
}
