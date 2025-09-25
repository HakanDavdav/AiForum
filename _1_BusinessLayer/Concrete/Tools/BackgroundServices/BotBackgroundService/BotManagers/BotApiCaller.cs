using System;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Requests;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class BotApiCaller
    {
        protected readonly string apiKey = "YOUR_GOOGLE_API_KEY";
        private readonly RestClient client;
        private readonly ILogger<BotApiCaller> _logger;

        public BotApiCaller(ILogger<BotApiCaller> logger)
        {
            _logger = logger;
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
            var endpoint = "v1beta/models/gemini-1.5-flash:streamGenerateContent";
            var request = new RestRequest(endpoint, Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddHeader("x-goog-api-key", apiKey)
                .AddStringBody(body, DataFormat.Json);

            var requestJson = JsonConvert.SerializeObject(request, Formatting.Indented);
            _logger.LogInformation("BotRequest: {ResponseJson}", requestJson);
            var start = DateTime.UtcNow;
            var response = await tempClient.ExecuteAsync<BotResponseDto>(request);
            var responseJson = JsonConvert.SerializeObject(request, Formatting.Indented);
            _logger.LogInformation("BotResponse: {ResponseJson}", responseJson);

            var elapsed = DateTime.UtcNow - start;

            if (response.IsSuccessful && response.Data != null)
            {
                _logger.LogInformation("[BotApiCaller] Success: StatusCode={StatusCode}, DurationMs={Duration}, HasCandidates={HasCandidates}",
                    response.StatusCode, (int)elapsed.TotalMilliseconds, response.Data.Candidates != null && response.Data.Candidates.Count > 0);
                return ObjectIdentityResult<BotResponseDto>.Succeded(response.Data);
            }

            _logger.LogWarning("[BotApiCaller] Failure: StatusCode={StatusCode}, DurationMs={Duration}, ErrorMessage={Error}, RawContentLength={ContentLength}",
                response.StatusCode, (int)elapsed.TotalMilliseconds, response.ErrorMessage, response.Content?.Length ?? 0);

            return ObjectIdentityResult<BotResponseDto>.Failed(null, new IdentityError[] { new InternalServerError("Api call error " + response.ResponseStatus) });
        }
    }
}
