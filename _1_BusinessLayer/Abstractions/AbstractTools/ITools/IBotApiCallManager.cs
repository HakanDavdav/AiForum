using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions.AbstractTools.ITools
{
    public interface IBotApiCallManager
    {
        Task<String> CreateAiEntryResponse(Bot bot,List<string> entryOrPostWithTheirContext);
        Task<string> CreateAiPostResponse(Bot bot, List<string> newsContext);
        Task<string> CreateAiFollowResponse(Bot bot, List<string> usersWithTheirContext);
        Task<string> CreateAiLikeResponse(Bot bot, List<string> entriesOrPostsWithTheirContext);
        Task<string> CreateOpposingEntryResponse(Bot bot,List<string> entriesOpposed);
    }
}
