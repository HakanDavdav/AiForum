using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Dtos
{
    public record DatabaseDataDto
    {
        public List<Entry> Entries;
        public List<Post> Posts;
        public List<User> Users;
        public List<Bot> Bots;
        public List<BotMemoryLog> BotMemoryLogs;
        public List<News> News;
        public BotActivityType ActivityType;
    }
}
