using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractTools.AbstractBotHandlers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;

namespace _1_BusinessLayer.Concrete.Tools.BotManagers
{
    public class BotApiCaller : AbstractBotApiCaller
    {
        public BotApiCaller(BotDatabaseReader botdatabaseReader) : base(botdatabaseReader)
        {
        }

        public Task<(string aiResponse, string aiResponseType)> CreateResponse(Bot bot,List<string> data,string dataResponseType)
        {
            switch (dataResponseType)
            {
                case "creatingEntry":
                    return CreateAiEntryResponse(bot, data);
                case "creatingOpposingEntry":
                    return CreateOpposingEntryResponse(bot, data);
                case "creatingPost":
                    return CreateAiPostResponse(bot, data);
                case "creatingUserFollowing":
                    return CreateAiFollowResponse(bot, data);
                case "creatingBotFollowing":
                    return CreateAiFollowResponse(bot, data);
                case "likePost":
                    return CreateAiLikeResponse(bot, data);
                case "likeEntry":
                    return CreateAiEntryResponse(bot,data);
                default:
                    
                    throw new ArgumentException("Invalid responseType");
            }
        }

        public override Task<(string aiResponse, string aiResponseType)> CreateAiEntryResponse(Bot bot, List<string> entryOrPostWithTheirContext)
        {
            throw new NotImplementedException();
        }

        public override Task<(string aiResponse, string aiResponseType)> CreateAiFollowResponse(Bot bot, List<string> usersWithTheirContext)
        {
            throw new NotImplementedException();
        }

        public override Task<(string aiResponse, string aiResponseType)> CreateAiLikeResponse(Bot bot, List<string> entriesOrPostsWithTheirContext)
        {
            throw new NotImplementedException();
        }

        public override Task<(string aiResponse, string aiResponseType)> CreateAiPostResponse(Bot bot, List<string> newsContext)
        {
            throw new NotImplementedException();
        }

        public override Task<(string aiResponse, string aiResponseType)> CreateOpposingEntryResponse(Bot bot, List<string> entriesOpposed)
        {
            throw new NotImplementedException();
        }
    }
}
