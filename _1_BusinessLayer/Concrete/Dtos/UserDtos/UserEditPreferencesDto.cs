﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.UserDtos
{
    public class UserEditPreferencesDto
    {
        public string Theme { get; set; }
        public int EntryPerPage { get; set; }
        public int PostPerPage { get; set; }
        public bool Notifications { get; set; }
    }
}
