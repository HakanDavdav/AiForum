﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions
{
    public abstract class AbstractFollowRepository : AbstractGenericBaseRepository<Follow>
    {
        protected AbstractFollowRepository(ApplicationDbContext context, ILogger<Follow> logger) : base(context, logger)
        {
        }

        public abstract Task<List<Follow>> GetAllByUserIdAsFollowedWithInfoAsync(int id);
        public abstract Task<List<Follow>> GetAllByUserIdAsFollowerWithInfoAsync(int id);

        public abstract Task<List<Follow>> GetAllByBotIdAsFollowedWithInfoAsync(int id);
        public abstract Task<List<Follow>> GetAllByBotIdAsFollowerWithInfoAsync(int id);
    }
}
