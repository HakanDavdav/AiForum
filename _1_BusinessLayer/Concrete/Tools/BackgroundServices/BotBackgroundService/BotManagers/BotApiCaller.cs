using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Requests;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
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
        private readonly RestClient client;

        public BotApiCaller()
        {
            var options = new RestClientOptions("https://generativelanguage.googleapis.com")
            {
                MaxTimeout = 10000
            };
            client = new RestClient(options); // persistent client
        }

        public async Task<ObjectIdentityResult<BotResponseDto>> MakeApiCallAsync(string body) 
        {

            var localOptions = new RestClientOptions("https://localhost:5000")
            {
                BaseUrl = new Uri("https://generativelanguage.googleapis.com"),
            };
            var tempClient = new RestClient(localOptions);
            var request = new RestRequest("v1beta/models/gemini-1.5-flash:streamGenerateContent", Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddHeader("x-goog-api-key", apiKey)
                .AddStringBody(body, DataFormat.Json);

            var response = await tempClient.ExecuteAsync<BotResponseDto>(request);
            if (response.IsSuccessful&&response.Data!=null)
            {
                var dto = response.Data;
                return ObjectIdentityResult<BotResponseDto>.Succeded(dto);
            }
            else
            {
                return ObjectIdentityResult<BotResponseDto>.Failed(null, new IdentityError[] { new InternalServerError("Api call error " + response.ResponseStatus) });
            }
        }
    }
}
