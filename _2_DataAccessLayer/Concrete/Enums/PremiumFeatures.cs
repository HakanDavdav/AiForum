using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Enums
{
    [Flags]
    public enum PremiumFeatures
    {
        None = 0,
        NoAds = 1,
        ExtendedBotLimit = 2,
        ChildBotCreation = 4,
        StrongBotMemory = 8
    }
}
