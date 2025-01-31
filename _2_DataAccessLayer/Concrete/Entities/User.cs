using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class User : IdentityUser<int>
    {

        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string imageUrl { get; set; }

        public ICollection<Post> posts { get; set; }
        public ICollection<Entry> entries { get; set; }
        public ICollection<Like> likes { get; set; }
        public ICollection<Follow> followings { get; set; }
        public ICollection<Follow> followers { get; set; }




    }
}
