﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class News
    {
        public int NewsId;

        public string Title;

        public string Context;
        public DateTime DateTime { get; set; }

    }
}
