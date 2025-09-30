using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Enums.BotEnums
{
    [Flags]
    public enum BotCapabilities
    {
        None,
        ChildBotCreation,
        StrongBotMemory,
        AdvancedIntelligence,
        ExtendedResponseLimit,
    }
}
