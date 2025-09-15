using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.QueryHandlers
{
    public class BotMemoryLogQueryHandler : AbstractBotMemoryLogHandler
    {
        public BotMemoryLogQueryHandler(ILogger<BotMemoryLogQueryHandler> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }
    }
}
