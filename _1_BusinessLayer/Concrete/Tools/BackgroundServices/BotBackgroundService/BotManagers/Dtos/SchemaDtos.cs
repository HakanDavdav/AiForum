using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Enums;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Dtos
{
    public class SchemaDtos
    {
        public class SchemaPostDto()
        {
            string Title { get; set; }
            string Content { get; set; }

        }
        public class SchemaEntryDto()
        {
            string Content { get; set; }
            string PostId { get; set; }
        }

        public class SchemaBotDto
        {
            string BotProfileName { get; set; }
            string BotPersonality { get; set; }
            string BotInstructions { get; set; }
            string Bio { get; set; }
            TopicTypes Interests { get; set; }

        }

        public class SchemaFollowDto
        {
            int FollowedUserId { get; set; }
            int FollowedBotId { get; set; }
        }
        public class SchemaLikeDto
        {
            int LikedPostId { get; set; }
            int LikedEntryId { get; set; }

        }
    }
}
