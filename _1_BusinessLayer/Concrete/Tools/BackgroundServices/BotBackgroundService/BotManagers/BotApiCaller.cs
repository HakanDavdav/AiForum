using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class BotApiCaller
    {
        protected readonly string apiKey = "YOUR_GOOGLE_API_KEY";
        protected readonly string apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:streamGenerateContent?alt=sse&key={{apiKey}}";

        public BotApiCaller() { }

        public Task<(string aiResponse, string aiResponseType)> CreateResponse(Bot bot, List<string> data, BotActivityTypes.BotActivityType dataResponseType)
        {
            if (bot == null) throw new ArgumentNullException(nameof(bot));
            if (data == null || data.Count == 0) throw new ArgumentException("Data list cannot be null or empty", nameof(data));
            switch (dataResponseType)
            {
                case BotActivityTypes.BotActivityType.BotCreatedEntry:
                case BotActivityTypes.BotActivityType.BotEntryLiked:
                    return CreateAiEntryResponse(bot, data);
                case BotActivityTypes.BotActivityType.BotCreatedPost:
                case BotActivityTypes.BotActivityType.BotPostLiked:
                    return CreateAiPostResponse(bot, data);
                case BotActivityTypes.BotActivityType.BotGainedFollower:
                case BotActivityTypes.BotActivityType.BotStartedFollow:
                    return CreateAiFollowResponse(bot, data);
                case BotActivityTypes.BotActivityType.BotLikedPost:
                case BotActivityTypes.BotActivityType.BotLikedEntry:
                    return CreateAiLikeResponse(bot, data);
                default:
                    throw new ArgumentException($"Invalid BotActivityType: {dataResponseType}");
            }
        }

        public async Task<(string aiResponse, string aiResponseType)> CreateAiEntryResponse(Bot bot, List<string> entryOrPostWithTheirContext)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    throw new NotImplementedException();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<(string aiResponse, string aiResponseType)> CreateAiFollowResponse(Bot bot, List<string> usersWithTheirContext)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    throw new NotImplementedException();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<(string aiResponse, string aiResponseType)> CreateAiLikeResponse(Bot bot, List<string> entriesOrPostsWithTheirContext)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    throw new NotImplementedException();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<(string aiResponse, string aiResponseType)> CreateAiPostResponse(Bot bot, List<string> newsContext)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    throw new NotImplementedException();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<(string aiResponse, string aiResponseType)> CreateOpposingEntryResponse(Bot bot, List<string> entriesOpposed)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    throw new NotImplementedException();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
