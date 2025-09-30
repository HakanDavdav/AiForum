using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Enums.OtherEnums
{
    [Flags]
    public enum PremiumFeatures
    {
        None, 
        NoAdsFeature,
        ExtendedBotLimitFeature,
        ChildBotCreationFeature,
        StrongBotMemoryFeature,
    }
}
