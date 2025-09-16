using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using RestSharp;
using RestSharp.Authenticators;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class BotApiCaller
    {
        protected readonly string apiKey = "YOUR_GOOGLE_API_KEY";
        protected readonly string apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:streamGenerateContent?alt=sse&key={{apiKey}}";

        public BotApiCaller() { }

       
        public ObjectIdentityResult<string> TopicPreferenceApiCallAsync(Bot bot)
        {
            var options
        }
        public async Task<ObjectIdentityResult<string>> BotActivityContentSelectorApiCallAsync(DatabaseDataDto databaseData, Bot bot) //Consider interests
        {
            var options = new RestClientOptions("https://api.twitter.com/1.1")
            {
                Authenticator = new HttpBasicAuthenticator("username", "password")
            };
            var client = new RestClient(options);
            var request = new RestRequest("/statuses/user_timeline.json", Method.Get);
            var response = await client.GetAsync(request);
        }
    }
}
