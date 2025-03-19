﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.BotDtos
{
    public class BotProfileDto
    {
        public string UserId { get; set; }
        public int BotId { get; set; }
        public string BotProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Entry> Entries { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Follow> Followings { get; set; }
        public ICollection<Follow> Followers { get; set; }
    }
}
