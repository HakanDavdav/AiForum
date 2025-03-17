using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Exceptions;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;

namespace _1_BusinessLayer.Concrete.Tools.BotManagers
{
    public class BotManager 
    {
        protected BotDatabaseReader _botDatabaseReader;
        protected BotApiCaller _botApiCaller;
        protected BotDatabaseWriter _botDatabaseWriter;
        protected BotResponseParser _botResponseParser;
        public BotManager(BotDatabaseReader botDatabaseReader,BotApiCaller botApiCaller,BotDatabaseWriter botDatabaseWriter,BotResponseParser botResponseParser)
        {
            _botDatabaseReader = botDatabaseReader;
            _botApiCaller = botApiCaller;
            _botDatabaseWriter = botDatabaseWriter;
            _botResponseParser = botResponseParser;
        }
        public async Task<ObjectIdentityResult<Notification>> BotDoAction(Bot bot)
        {
            if (bot.DailyActionsCheck == false)
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
                    var (aiResponse, aiResponseType) = await _botApiCaller.CreateResponse(bot, data, dataResponseType);
                    var (requiredId, filteredAiResponse, parseResponseType) = await _botResponseParser.Parse(aiResponse,aiResponseType);
                    var notification = await _botDatabaseWriter.WriteOnDatabase(bot,requiredId,filteredAiResponse,parseResponseType);
                    return ObjectIdentityResult<Notification>.Succeded(notification);
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
                    var (data, dataResponseType) = await _botDatabaseReader.GetModelDataAsync(probabilitySet);
                    var (aiResponse, aiResponseType) = await _botApiCaller.CreateResponse(bot, data, dataResponseType);
                    var (requiredId, filteredAiResponse, parseResponseType) = await _botResponseParser.Parse(aiResponse, aiResponseType);
                    var notification = await _botDatabaseWriter.WriteOnDatabase(bot, requiredId, filteredAiResponse, parseResponseType);
                    return ObjectIdentityResult<Notification>.Succeded(notification);
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
                    var (data, dataResponseType) = await _botDatabaseReader.GetModelDataAsync(probabilitySet);
                    var (aiResponse, aiResponseType) = await _botApiCaller.CreateResponse(bot, data, dataResponseType);
                    var (requiredId, filteredAiResponse, parseResponseType) = await _botResponseParser.Parse(aiResponse, aiResponseType);
                    var notification = await _botDatabaseWriter.WriteOnDatabase(bot, requiredId, filteredAiResponse, parseResponseType);
                    return ObjectIdentityResult<Notification>.Succeded(notification);
                }
                else
                {
                    throw new InvalidBotModeException();    
                }
            }
            else
            {
                return ObjectIdentityResult<Notification>.Failed(null,new IdentityError[] { new UnauthorizedError("Daily limit has reached!!") });
            }
            
                                   
        }
    }
}
