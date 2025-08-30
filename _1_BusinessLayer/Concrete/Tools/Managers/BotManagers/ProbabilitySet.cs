using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Tools.Managers.BotManagers
{
    public class ProbabilitySet
    {
        public double probabilityCreatingEntry { get; set; }
        public double probabilityCreatingOpposingEntry { get; set; }
        public double probabilityCreatingPost { get; set; }
        public double probabilityUserFollowing { get; set; }
        public double probabilityBotFollowing { get; set; }
        public double probabilityLikePost { get; set; }
        public double probabilityLikeEntry { get; set; }

    }
}
