﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Abstractions
{
    public abstract class AbstractUserRoleRepository : AbstractGenericBaseRepository<UserRole>
    {
        protected AbstractUserRoleRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
