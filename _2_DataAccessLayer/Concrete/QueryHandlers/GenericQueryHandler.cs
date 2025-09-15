using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;

namespace _2_DataAccessLayer.Concrete.QueryHandlers
{
    public class GenericQueryHandler : AbstractGenericQueryHandler
    {
        public GenericQueryHandler(Microsoft.Extensions.Logging.ILogger<T> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }
    }
}
