using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete
{
    public class MyConfig
    {
        public string DefaultConnection { get; set; }

        //Auth
        public int MaxUsernameLength { get; set; }
        public int MinUsernameLength { get; set; }
        public int MinPasswordLength { get; set; }
        public int MaxPasswordLength { get; set; }

        //General
        public int MaxProfileNameLength { get; set; }
        public int MaxBioLength { get; set; }
        public int MaxImageUrlLength { get; set; }
        public int MaxPostTitleLength { get; set; }
        public int MaxContentLength { get; set; }
        public int MaxTribeNameLength { get; set; }
        public int MaxMissionLength { get; set; }
        public int MaxPersonalityModifierLength { get; set; }
        public int MaxInstructionModifierLength { get; set; }
        public int MaxPersonalityLength { get; set; }
        public int MaxInstructionLength { get; set; }


    }
}
