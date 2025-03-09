using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.BotHandlers.BotManagers;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.BotHandlers.BotDecorators
{
    public class SimpleBot
    {
        protected BotApiCallManager _apiManager {  get; set; }
        protected Bot _bot { get; set; }
        public SimpleBot(Bot bot)
        {
            _bot = bot;
        }
        public virtual void Deploy()
        {
            
        }
    }
}
