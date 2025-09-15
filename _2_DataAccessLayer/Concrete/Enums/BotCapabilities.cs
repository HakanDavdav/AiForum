using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Enums
{
    [Flags]
    public enum BotCapabilities
    {
        None = 0,
        ChildBotCreation = 1,
        StrongBotMemory = 2,
        AdvancedIntelligence = 4,
        ExtendedResponseLimit = 8,
    }
}
