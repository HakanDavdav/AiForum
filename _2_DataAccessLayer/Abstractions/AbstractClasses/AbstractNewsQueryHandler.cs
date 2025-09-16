using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.AbstractClasses
{
    public class AbstractNewsQueryHandler : AbstractGenericBaseQueryHandler<News>
    {
        public AbstractNewsQueryHandler(ILogger<News> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }
    }
}
