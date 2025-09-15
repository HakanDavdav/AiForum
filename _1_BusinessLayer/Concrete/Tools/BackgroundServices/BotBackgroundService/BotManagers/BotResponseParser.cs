using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class BotResponseParser
    {
        public Dictionary<TopicTypes, decimal> ParseTopicPreferencesResponse(string response){

            throw new NotImplementedException();
        }
        public Entry ParseCreateEntryResponse(string response) { }
        public Post ParseCreatePostResponse(string response) { }
        public Follow ParseFollowRequestResponse(string response) { }
        public Entry ParseOpposingEntryResponse(string response) { }
        public Like ParseLikeEntryResponse(string response) { }
        public Post ParseLikePostResponse(string response) { }



    }
}
