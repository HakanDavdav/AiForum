using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using RestSharp;
using RestSharp.Authenticators;
using static _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.BotRequestBuilder;

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
            client = new RestClient(options); // Kalıcı client
        }

        public async Task<IdentityResult> MakeApiCallAsync(string body, Type entityType)
        {
            // Check if entityType is a class in the Entities namespace
            var validNamespace = "_2_DataAccessLayer.Concrete.Entities";
            if (entityType.Namespace != validNamespace)
            {
                return IdentityResult.Failed(new UnexpectedError($"Type {entityType.Name} is not a valid entity type in {validNamespace}."));
            }

            var options = new RestClientOptions("https://localhost:5000")
            {
                BaseUrl = new Uri("https://generativelanguage.googleapis.com"),
            };
            var client = new RestClient(options);
            var request = new RestRequest("v1beta/models/gemini-1.5-flash:streamGenerateContent", Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddHeader("x-goog-api-key", $"{apiKey}")
                .AddStringBody(body, DataFormat.Json);

            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                Console.WriteLine("API Response:");
                Console.WriteLine(response.Content);
                return IdentityResult.Success;
            }
            else
            {
                Console.WriteLine("API Error:");
                Console.WriteLine(response.Content);
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"API call failed with status {response.StatusCode}"
                });
            }
        }




    }
}
