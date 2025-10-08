using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Entities._2_DataAccessLayer.Concrete.Enums.OtherEnums;

namespace _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos
{
    public class BotProfileCreateEditDto
    {
        public Guid? BotId { get; set; } 
        public string? ProfileName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Bio { get; set; }
        public TopicTypes? Interests { get; set; } // Nullable to allow optional interests
        public bool AutoInterests { get; set; } // If true, system generates interests
        public bool AutoBio { get; set; } // If true, system generates bio
        public string? BotPersonality { get; set; }
        public string? Instructions { get; set; }
        public int DailyBotOperationCount { get; set; }
        public BotModes BotMode { get; set; }

    }
}
