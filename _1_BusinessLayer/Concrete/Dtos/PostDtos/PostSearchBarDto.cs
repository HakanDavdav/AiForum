﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos.PostDtos
{
    public class PostSearchBarDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public ICollection<Like> Likes { get; set; }

    }
}
