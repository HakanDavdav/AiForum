﻿using System;
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
        public string? ImageUrl { get; set; }
        [AllowNull]
        public string? City { get; set; }
        [AllowNull]
        public int? ConfirmationCode { get; set; }
        [AllowNull]
        public string? ProfileName { get; set; }
        [AllowNull]
        public string? Personality { get; set; }
        


        public ICollection<Post> Posts { get; set; }
        public ICollection<Entry> Entries { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Follow> Followings { get; set; }
        public ICollection<Follow> Followers { get; set; }





    }
}
