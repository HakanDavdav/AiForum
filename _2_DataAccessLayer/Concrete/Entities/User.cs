using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class User
    {

        public int userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public ICollection<Post> posts { get; set; }
        public ICollection<Entry> entries { get; set; }
        public ICollection<Like> likes { get; set; }
        public ICollection<Follow> following { get; set; }
        public ICollection<Follow> followers { get; set; }




    }
}
