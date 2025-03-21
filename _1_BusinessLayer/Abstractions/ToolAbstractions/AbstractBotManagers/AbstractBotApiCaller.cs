using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.ITools;
using _1_BusinessLayer.Concrete.Tools.BotManagers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions.AbstractTools.AbstractBotHandlers
{
    public abstract class AbstractBotApiCaller : IBotApiCaller
    {
        protected readonly string apiKey
           = "YOUR_GOOGLE_API_KEY";
        protected readonly string apiUrl
            = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:streamGenerateContent?alt=sse&key={apiKey}";
        protected readonly BotDatabaseReader _botdatabaseReader;
        protected AbstractBotApiCaller(BotDatabaseReader botdatabaseReader)
        {
            _botdatabaseReader = botdatabaseReader;
        }

        public abstract Task<(string aiResponse, string aiResponseType)> CreateAiEntryResponse(Bot bot, List<string> entryOrPostWithTheirContext);
        public abstract Task<(string aiResponse, string aiResponseType)> CreateAiFollowResponse(Bot bot, List<string> usersWithTheirContext);
        public abstract Task<(string aiResponse, string aiResponseType)> CreateAiLikeResponse(Bot bot, List<string> entriesOrPostsWithTheirContext);
        public abstract Task<(string aiResponse, string aiResponseType)> CreateAiPostResponse(Bot bot, List<string> newsContext);
        public abstract Task<(string aiResponse, string aiResponseType)> CreateOpposingEntryResponse(Bot bot, List<string> entriesOpposed);
    }
}
