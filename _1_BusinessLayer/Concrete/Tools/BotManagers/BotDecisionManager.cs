using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Tools.BotManagers
{
    public class BotDecisionManager 
    {
        protected BotDatabaseReader _botDatabaseReader;
        protected BotApiCallManager _botApiCallManager;
        protected BotOperationManager _botOperationManager;
        protected AiResponseParser _aiResponseParser;
        public BotDecisionManager(BotDatabaseReader botDatabaseReader,BotApiCallManager botApiCallManager,BotOperationManager botOperationManager,AiResponseParser aiResponseParser)
        {
            _botDatabaseReader = botDatabaseReader;
            _botApiCallManager = botApiCallManager;
            _botOperationManager = botOperationManager;
            _aiResponseParser = aiResponseParser;
        }
        public async Task<IdentityResult> BotDoAction(Bot bot)
        {
            if (bot.DailyMessageCheck == false)
            {
                if(bot.Mode == "OPPOSİNG")
                {
                    var probabilitySet = new ProbabilitySet()
                    {
                         probabilityCreatingEntry = 0.1, 
                         probabilityCreatingOpposingEntry = 0.60,
                         probabilityCreatingPost = 0.05,
                         probabilityUserFollowing = 0.05,
                         probabilityBotFollowing = 0.05,
                         probabilityLikePost = 0.075,
                         probabilityLikeEntry = 0.075
                    };
                    var (data,dataResponseType) = await _botDatabaseReader.GetModelDataAsync(probabilitySet);
                    var (aiResponse, aiResponseType) = await _botApiCallManager.CreateResponse(bot, data, dataResponseType);
                    var (id, filteredAiResponse) = _aiResponseParser.
                }
                else if(bot.Mode == "INDEPENDENT")
                {
                    var probabilitySet = new ProbabilitySet()
                    {
                        probabilityCreatingEntry = 0.1,
                        probabilityCreatingOpposingEntry = 0.60,
                        probabilityCreatingPost = 0.05,
                        probabilityUserFollowing = 0.05,
                        probabilityBotFollowing = 0.05,
                        probabilityLikePost = 0.075,
                        probabilityLikeEntry = 0.075
                    };
                    var (data, responseType) = await _botDatabaseReader.GetModelDataAsync(probabilitySet);
                }
                else if (bot.Mode == "DEFAULT")
                {
                    var probabilitySet = new ProbabilitySet()
                    {
                        probabilityCreatingEntry = 0.1,
                        probabilityCreatingOpposingEntry = 0.60,
                        probabilityCreatingPost = 0.05,
                        probabilityUserFollowing = 0.05,
                        probabilityBotFollowing = 0.05,
                        probabilityLikePost = 0.075,
                        probabilityLikeEntry = 0.075
                    };
                    var (data, responseType) = await _botDatabaseReader.GetModelDataAsync(probabilitySet);
                }
                else
                {
                    IdentityResult.Failed(new UnexpectedError("Not valid bot mode"));
                }
            }
            return IdentityResult.Failed(new ForbiddenError("Exceeded daily limit"));
                                   
        }
    }
}
