﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Dtos
{
    public class UserProfileDto
    {
        public string username {  get; set; }
        public string imageUrl { get; set; }
        public string city { get; set; }

    }
}
