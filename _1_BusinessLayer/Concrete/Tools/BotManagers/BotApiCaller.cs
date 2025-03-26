using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;

namespace _1_BusinessLayer.Concrete.Tools.BotManagers
{
    public class BotApiCaller 
    {
        protected readonly string apiKey
   = "YOUR_GOOGLE_API_KEY";
        protected readonly string apiUrl
            = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:streamGenerateContent?alt=sse&key={"apiKey"}";

        public Task<(string aiResponse, string aiResponseType)> CreateResponse(Bot bot,List<string> data,string dataResponseType)
        {
            if (bot == null) throw new ArgumentNullException(nameof(bot));
            if (data == null || data.Count == 0) throw new ArgumentException("Data list cannot be null or empty", nameof(data));
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

        public async Task<(string aiResponse, string aiResponseType)> CreateAiEntryResponse(Bot bot, List<string> entryOrPostWithTheirContext)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("apiUrl");
                    response.EnsureSuccessStatusCode(); // HTTP 200-299 değilse hata fırlatır
                    throw new NotImplementedException();
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("API request failed", ex);
                }
            }
        }

        public async Task<(string aiResponse, string aiResponseType)> CreateAiFollowResponse(Bot bot, List<string> usersWithTheirContext)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("apiUrl");
                    response.EnsureSuccessStatusCode(); // HTTP 200-299 değilse hata fırlatır
                    throw new NotImplementedException();

                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("API request failed", ex);
                }
            }
        }

        public async Task<(string aiResponse, string aiResponseType)> CreateAiLikeResponse(Bot bot, List<string> entriesOrPostsWithTheirContext)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("apiUrl");
                    response.EnsureSuccessStatusCode(); // HTTP 200-299 değilse hata fırlatır
                    throw new NotImplementedException();

                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("API request failed", ex);
                }
            }
        }

        public async Task<(string aiResponse, string aiResponseType)> CreateAiPostResponse(Bot bot, List<string> newsContext)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("apiUrl");
                    response.EnsureSuccessStatusCode(); // HTTP 200-299 değilse hata fırlatır
                    throw new NotImplementedException();

                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("API request failed", ex);
                }
            }
        }

        public async Task<(string aiResponse, string aiResponseType)> CreateOpposingEntryResponse(Bot bot, List<string> entriesOpposed)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("apiUrl");
                    response.EnsureSuccessStatusCode(); // HTTP 200-299 değilse hata fırlatır
                    throw new NotImplementedException();

                }
                catch (HttpRequestException ex)
                {
                    throw;
                }
            }
        }
    }
}
