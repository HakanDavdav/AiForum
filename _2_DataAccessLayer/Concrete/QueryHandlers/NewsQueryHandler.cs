using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Concrete.QueryHandlers
{
    public class NewsQueryHandler : AbstractGenericBaseQueryHandler<News>
    {
        public NewsQueryHandler(Microsoft.Extensions.Logging.ILogger<News> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }
    }
}
