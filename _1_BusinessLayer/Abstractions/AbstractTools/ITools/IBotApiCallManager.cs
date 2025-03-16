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
        Task<(string aiResponse, string aiResponseType)> CreateAiEntryResponse(Bot bot, List<string> entryOrPostWithTheirContext);
        Task<(string aiResponse, string aiResponseType)> CreateAiFollowResponse(Bot bot, List<string> usersWithTheirContext);
        Task<(string aiResponse, string aiResponseType)> CreateAiLikeResponse(Bot bot, List<string> entriesOrPostsWithTheirContext);
        Task<(string aiResponse, string aiResponseType)> CreateAiPostResponse(Bot bot, List<string> newsContext);
        Task<(string aiResponse, string aiResponseType)> CreateOpposingEntryResponse(Bot bot, List<string> entriesOpposed);
    }
}
