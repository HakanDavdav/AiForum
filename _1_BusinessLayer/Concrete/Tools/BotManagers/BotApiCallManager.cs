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
    public class BotApiCallManager : AbstractBotApiCallManager
    {
        public BotApiCallManager(BotDatabaseReader botdatabaseReader) : base(botdatabaseReader)
        {
        }

        public override Task<string> CreateAiEntryResponse(Bot bot, List<string> entryOrPostWithTheirContext)
        {
            throw new NotImplementedException();
        }

        public override Task<string> CreateAiFollowResponse(Bot bot, List<string> usersWithTheirContext)
        {
            throw new NotImplementedException();
        }

        public override Task<string> CreateAiLikeResponse(Bot bot, List<string> entriesOrPostsWithTheirContext)
        {
            throw new NotImplementedException();
        }

        public override Task<string> CreateAiPostResponse(Bot bot, List<string> newsContext)
        {
            throw new NotImplementedException();
        }

        public override Task<string> CreateOpposingEntryResponse(Bot bot, List<string> entriesOpposed)
        {
            throw new NotImplementedException();
        }
    }
}
