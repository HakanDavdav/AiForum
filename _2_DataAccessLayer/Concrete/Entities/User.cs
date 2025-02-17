using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class User : IdentityUser<int>
    {
        [AllowNull]
        public string? imageUrl { get; set; }
        [AllowNull]
        public string? city { get; set; }
        [AllowNull]
        public int? confirmationCode { get; set; }


        public ICollection<Post> posts { get; set; }
        public ICollection<Entry> entries { get; set; }
        public ICollection<Like> likes { get; set; }
        public ICollection<Follow> followings { get; set; }
        public ICollection<Follow> followers { get; set; }





    }
}
