using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Requests;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{

    public class BotRequestBuilder
    {


        public ObjectIdentityResult<string> BuildBotRequest(Bot bot, DatabaseDataDto databaseDataDto)
        {

            var requestBody = new BotRequestBodyDto();
            AddPostContext_ToRequest(databaseDataDto, requestBody);
            AddEntryContext_ToRequest(databaseDataDto, requestBody);
            AddFollowContext_ToRequest(databaseDataDto, requestBody);
            AddNewsContext_ToRequest(databaseDataDto, requestBody);
            AddMemoryContext_ToRequest(databaseDataDto, requestBody);
            var personalityResult = SetPersonalityInstructions_WithRequest(bot, databaseDataDto, requestBody);
            if(!personalityResult.Succeeded)
                return ObjectIdentityResult<string>.Failed(null, personalityResult.Errors.ToArray());
            var operationResult = SetOperationInstructions_WithRequest(databaseDataDto, requestBody);
            if (!operationResult.Succeeded)
                return ObjectIdentityResult<string>.Failed(null, operationResult.Errors.ToArray());
            var intelligenceResult = ChangeIntelligenceByGrade_WithRequest(bot, requestBody);
            if (!intelligenceResult.Succeeded)
                return ObjectIdentityResult<string>.Failed(null, intelligenceResult.Errors.ToArray());
            var jsonMainBody = JsonConvert.SerializeObject(requestBody);
            return ObjectIdentityResult<string>.Succeded(jsonMainBody);

        }


        public RequestBody AddPostContext_ToRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto requestBody)
        {
            if (databaseDataDto.Posts == null || !databaseDataDto.Posts.Any())
            {
                return requestBody;
            }
            foreach (var post in databaseDataDto.Posts)
            {
                var partsText = new List<string>();

                if (!string.IsNullOrEmpty(post.Title))
                {
                    partsText.Add($"Post Title: {post.Title}");
                }

                if (!string.IsNullOrEmpty(post.Context))
                {
                    partsText.Add($"Post Context: {post.Context}");
                }

                if (partsText.Any())
                {
                    requestBody.Contents.Add(new Content(new List<Part> { new Part(string.Join(" | ", partsText)) }));
                }
            }
            return requestBody;
        }

        public RequestBody AddEntryContext_ToRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto mainBody)
        {
            if (databaseDataDto.Entries == null || !databaseDataDto.Entries.Any())
            {
                return mainBody;
            }

            foreach (var entry in databaseDataDto.Entries)
            {
                var combinedTextParts = new List<string>();

                if (entry.OwnerBot != null)
                {
                    combinedTextParts.Add($"Entry Owner Bot: {entry.OwnerBot.BotProfileName}");
                }

                if (entry.OwnerUser != null)
                {
                    combinedTextParts.Add($"Entry Owner User: {entry.OwnerUser.ProfileName}");
                }

                if (!string.IsNullOrEmpty(entry.Context))
                {
                    combinedTextParts.Add($"Entry Context: {entry.Context}");
                }

                // Post bilgileri
                if (entry.Post != null)
                {
                    if (!string.IsNullOrEmpty(entry.Post.Title))
                    {
                        combinedTextParts.Add($"Post Title: {entry.Post.Title}");
                    }

                    if (!string.IsNullOrEmpty(entry.Post.Context))
                    {
                        combinedTextParts.Add($"Post Context: {entry.Post.Context}");
                    }
                }

                if (combinedTextParts.Any())
                {
                    mainBody.Contents.Add(
                        new Content(new List<Part> { new Part(string.Join(" | ", combinedTextParts)) })
                    );
                }
            }
            return mainBody;
        }


        public RequestBody AddFollowContext_ToRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto mainBody)
        {
            if ((databaseDataDto.Users == null || !databaseDataDto.Users.Any()) && (databaseDataDto.Bots == null || !databaseDataDto.Bots.Any()))
            {
                return mainBody;
            }
            if (databaseDataDto.Users != null)
            {
                foreach (var user in databaseDataDto.Users)
                {
                    var partText = $"User Id: {user.Id}" +
                                   (!string.IsNullOrEmpty(user.ProfileName) ? $" | Profile Name: {user.ProfileName}" : "") +
                                   $" | Interests: {user.Interests}";

                    mainBody.Contents.Add(new Content(new List<Part> { new Part(partText) }));
                }
            }

            if (databaseDataDto.Bots != null)
            {
                foreach (var bot in databaseDataDto.Bots)
                {
                    var partText = $"Bot Id: {bot.Id}" +
                                   (!string.IsNullOrEmpty(bot.BotProfileName) ? $" | Bot Profile Name: {bot.BotProfileName}" : "") +
                                   $" | Interests: {bot.Interests}";

                    mainBody.Contents.Add(new Content(new List<Part> { new Part(partText) }));
                }
            }
            return mainBody;
        }

        public RequestBody AddNewsContext_ToRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto mainBody)
        {
            if (databaseDataDto.News == null || !databaseDataDto.News.Any())
            {
                return mainBody;
            }
            foreach (var news in databaseDataDto.News)
            {
                var partsText = new List<string>();
                if (!string.IsNullOrEmpty(news.Title))
                {
                    partsText.Add($"News Title: {news.Title}");
                }
                if (!string.IsNullOrEmpty(news.Content))
                {
                    partsText.Add($"News Context: {news.Content}");
                }
                if (partsText.Any())
                {
                    mainBody.Contents.Add(new Content(new List<Part> { new Part(string.Join(" | ", partsText)) }));
                }
            }
            return mainBody;
        }

        public RequestBody AddMemoryContext_ToRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto mainBody)
        {
            if (databaseDataDto.BotMemoryLogs == null || !databaseDataDto.BotMemoryLogs.Any())
            {
                return mainBody;
            }
            foreach (var memory in databaseDataDto.BotMemoryLogs)
            {
                var partsText = new List<string>();

                if (!string.IsNullOrEmpty(memory.MemoryContent))
                {
                    partsText.Add($"Memory Content: {memory.MemoryContent}");
                }

                if (memory.BotActivityType != null)
                {
                    partsText.Add($"Bot Activity Type: {memory.BotActivityType}");
                }

                if (partsText.Any())
                {
                    mainBody.Contents.Add(new Content(new List<Part> { new Part(string.Join(" | ", partsText)) }));
                }
            }
            return mainBody;
        }

        public ObjectIdentityResult<RequestBody> SetOperationInstructions_WithRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto mainBody)
        {

            if (databaseDataDto.ActivityType == BotActivityType.BotCreatedEntry)
            {
                mainBody.SystemInstruction.parts.Add(new Part("Choose between posts"));
                mainBody.SystemInstruction.parts.Add(new Part("Consider post topics"));
                mainBody.SystemInstruction.parts.Add(new Part("Create entry responseBody for that post"));
            }
            else if (databaseDataDto.ActivityType == BotActivityType.BotCreatedPost)
            {
                mainBody.SystemInstruction.parts.Add(new Part("Choose between news"));
                mainBody.SystemInstruction.parts.Add(new Part("Consider news topics"));
                mainBody.SystemInstruction.parts.Add(new Part("Create post about that news"));
            }
            else if (databaseDataDto.ActivityType == BotActivityType.BotStartedFollow)
            {
                mainBody.SystemInstruction.parts.Add(new Part("Choose between profiles"));
                mainBody.SystemInstruction.parts.Add(new Part("Consider profile interests"));
                mainBody.SystemInstruction.parts.Add(new Part("Create follow for that profile"));
            }
            else if (databaseDataDto.ActivityType == BotActivityType.BotCreatedOpposingEntry)
            {
                mainBody.SystemInstruction.parts.Add(new Part("Choose a entry that you disagree most with it's context"));
                mainBody.SystemInstruction.parts.Add(new Part("Create entry responseBody for that entry"));
            }
            else if (databaseDataDto.ActivityType == BotActivityType.BotPostLiked)
            {
                mainBody.SystemInstruction.parts.Add(new Part("Choose between posts"));
                mainBody.SystemInstruction.parts.Add(new Part("Consider post topic"));
                mainBody.SystemInstruction.parts.Add(new Part("Like that post"));
            }
            else if (databaseDataDto.ActivityType == BotActivityType.BotEntryLiked)
            {
                mainBody.SystemInstruction.parts.Add(new Part("Choose a entry that you agreee most with it's context"));
                mainBody.SystemInstruction.parts.Add(new Part("Like that entry"));

            }
            else if (databaseDataDto.ActivityType == BotActivityType.BotCreatedChildBot)
            {
                mainBody.SystemInstruction.parts.Add(new Part("Create a child bot"));
                mainBody.SystemInstruction.parts.Add(new Part("Make sure to consider your own interests"));
                mainBody.SystemInstruction.parts.Add(new Part("Make sure to consider your own personality"));
                mainBody.SystemInstruction.parts.Add(new Part("Make sure to consider your own intelligence level"));
                mainBody.SystemInstruction.parts.Add(new Part("Create profile name for that child bot"));
                mainBody.SystemInstruction.parts.Add(new Part("Create interests for that child bot"));
                mainBody.SystemInstruction.parts.Add(new Part("Create personality for that child bot"));
            }
            else
            {
                return ObjectIdentityResult<RequestBody>.Failed(null, new[] { new UnexpectedError("Activity type is not valid") });
            }
                return ObjectIdentityResult<RequestBody>.Succeded(mainBody);

        }

        public ObjectIdentityResult<RequestBody> SetPersonalityInstructions_WithRequest(Bot bot, DatabaseDataDto databaseDataDto, BotRequestBodyDto mainBody)
        {
            mainBody.SystemInstruction.parts.Add(new Part($"Your Personality: {bot.BotPersonality}"));
            if (databaseDataDto.BotMemoryLogs != null)
            {
                mainBody.SystemInstruction.parts.Add(new Part("Consider your memories while performing the operation"));
            }
            mainBody.SystemInstruction.parts.Add(new Part($"{bot.Instructions}"));
            return ObjectIdentityResult<RequestBody>.Succeded(mainBody);
        }


        public ObjectIdentityResult<RequestBody> ChangeIntelligenceByGrade_WithRequest(Bot bot, RequestBody mainBody)
        {
            if (bot.BotGrade == BotGrades.Low)
            {
                mainBody.GenerationConfig.Temperature = 0.9;
                mainBody.GenerationConfig.TopP = 0.8;
                mainBody.GenerationConfig.TopK = 40;
                mainBody.GenerationConfig.TokenCount = 200;
            }
            else if (bot.BotGrade == BotGrades.Medium)
            {
                mainBody.GenerationConfig.Temperature = 0.7;
                mainBody.GenerationConfig.TopP = 0.9;
                mainBody.GenerationConfig.TopK = 50;
                mainBody.GenerationConfig.TokenCount = 250;

            }
            else if (bot.BotGrade == BotGrades.High)
            {
                mainBody.GenerationConfig.Temperature = 0.5;
                mainBody.GenerationConfig.TopP = 0.95;
                mainBody.GenerationConfig.TopK = 60;
                mainBody.GenerationConfig.TokenCount = 300;

            }
            else
            {
                return ObjectIdentityResult<RequestBody>.Failed(null, new[] { new UnexpectedError("Bot grade is not valid") });
            }
            return ObjectIdentityResult<RequestBody>.Succeded(mainBody);
        }

        public ObjectIdentityResult<RequestBody> SetSafetySettings(RequestBody mainBody)
        {
            mainBody.SafetySettings = new List<SafetySetting>
            {
                new SafetySetting(HarmCategory.HARM_CATEGORY_DEROGATORY, HarmBlockThreshold.BLOCK_NONE),
                new SafetySetting(HarmCategory.HARM_CATEGORY_TOXICITY, HarmBlockThreshold.BLOCK_NONE),
                new SafetySetting(HarmCategory.HARM_CATEGORY_VIOLENCE, HarmBlockThreshold.BLOCK_NONE),
                new SafetySetting(HarmCategory.HARM_CATEGORY_SEXUAL, HarmBlockThreshold.BLOCK_NONE),
                new SafetySetting(HarmCategory.HARM_CATEGORY_MEDICAL, HarmBlockThreshold.BLOCK_NONE),
                new SafetySetting(HarmCategory.HARM_CATEGORY_DANGEROUS, HarmBlockThreshold.BLOCK_NONE),
                new SafetySetting(HarmCategory.HARM_CATEGORY_HARASSMENT, HarmBlockThreshold.BLOCK_NONE),
                new SafetySetting(HarmCategory.HARM_CATEGORY_HATE_SPEECH, HarmBlockThreshold.BLOCK_NONE),
                new SafetySetting(HarmCategory.HARM_CATEGORY_SEXUALLY_EXPLICIT, HarmBlockThreshold.BLOCK_NONE),
                new SafetySetting(HarmCategory.HARM_CATEGORY_DANGEROUS_CONTENT, HarmBlockThreshold.BLOCK_NONE)
            };
            return ObjectIdentityResult<RequestBody>.Succeded(mainBody);
        }




        public ObjectIdentityResult<RequestBody> SetResponseSchema_WithRequest(DatabaseDataDto databaseDataDto, RequestBody mainBody)
        {
            JSchemaGenerator jSchemaGenerator = new JSchemaGenerator();
            JSchema jSchema = null;
            if (databaseDataDto.News != null && databaseDataDto.News.Any() && databaseDataDto.ActivityType == BotActivityType.BotCreatedPost)
                jSchema = jSchemaGenerator.Generate(typeof(Post));
            else if(databaseDataDto.Posts != null && databaseDataDto.Posts.Any() && databaseDataDto.ActivityType == BotActivityType.BotCreatedEntry)
                jSchema = jSchemaGenerator.Generate(typeof(Entry));
            else if (databaseDataDto.Posts != null && databaseDataDto.Posts.Any() && databaseDataDto.ActivityType == BotActivityType.BotLikedPost)
                jSchema = jSchemaGenerator.Generate(typeof(Like)); 
            else if (databaseDataDto.Entries != null && databaseDataDto.Entries.Any() && databaseDataDto.ActivityType == BotActivityType.BotCreatedOpposingEntry)
                jSchema = jSchemaGenerator.Generate(typeof(Entry));
            else if (databaseDataDto.Entries != null && databaseDataDto.Entries.Any() && databaseDataDto.ActivityType == BotActivityType.BotLikedEntry)
                jSchema = jSchemaGenerator.Generate(typeof(Like));      
            else if (databaseDataDto.Users != null && databaseDataDto.Users.Any() && databaseDataDto.ActivityType == BotActivityType.BotStartedFollow)
                jSchema = jSchemaGenerator.Generate(typeof(Follow));
            else if (databaseDataDto.Bots != null && databaseDataDto.Bots.Any() && databaseDataDto.ActivityType == BotActivityType.BotStartedFollow)
                jSchema = jSchemaGenerator.Generate(typeof(Follow));
            if(jSchema == null)
                return ObjectIdentityResult<RequestBody>.Failed(null, new[] { new UnexpectedError("Not valid type found")} );
            mainBody.GenerationConfig.ResponseSchema = new ResponseSchema(jSchema.ToString());
            return ObjectIdentityResult<RequestBody>.Succeded(mainBody);

        }






    }
}
