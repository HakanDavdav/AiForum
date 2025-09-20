using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Microsoft.IdentityModel.Tokens;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Requests
{
    public class Requests
    {
        public record Part([property: JsonPropertyName("text")] string Text);
        public record SystemInstruction(List<Part?> parts);
        public record Content(List<Part?> parts);
        public record ResponseSchema(string Type);

        public class GenerationConfig
        {
            public ResponseSchema ResponseSchema { get; set; }
            [JsonPropertyName("maxOutputTokens")]
            public int TokenCount { get; set; }
            [JsonPropertyName("temperature")]
            public double Temperature { get; set; }
            [JsonPropertyName("topP")]
            public double TpNumber { get; set; }
            [JsonPropertyName("topK")]
            public double TkNumber { get; set; }

            public GenerationConfig(ResponseSchema responseSchema, int tokenCount, double temperature, double tpNumber, double tkNumber)
            {
                ResponseSchema = responseSchema;
                TokenCount = tokenCount;
                Temperature = temperature;
                TpNumber = tpNumber;
                TkNumber = tkNumber;
            }
        }

        public class MainBody
        {
            public SystemInstruction SystemInstruction { get; set; }
            public List<Content> Contents { get; set; }
            public GenerationConfig GenerationConfig { get; set; }

            public MainBody(SystemInstruction systemInstruction, List<Content> contents, GenerationConfig generationConfig)
            {
                SystemInstruction = systemInstruction;
                Contents = contents;
                GenerationConfig = generationConfig;
            }
        }

        public MainBody GetMainBody()
        {
            return new MainBody(
                new SystemInstruction(null),
                new List<Content> { new Content(null) },
                new GenerationConfig(
                    new ResponseSchema("text"),
                    tokenCount: 100, //default
                    temperature: 0.7, //default
                    tpNumber: 0.9, //default
                    tkNumber: 2) //default
            );
        }



        public void AddPostContext_ToRequest(DatabaseDataDto databaseDataDto, MainBody mainBody)
        {
            if (databaseDataDto.Posts == null || !databaseDataDto.Posts.Any())
            {
                return;
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
                    mainBody.Contents.Add(new Content(new List<Part> { new Part(string.Join(" | ", partsText)) }));
                }
            }
        }

        public void AddEntryContext_ToRequest(DatabaseDataDto databaseDataDto, MainBody mainBody)
        {
            if (databaseDataDto.Entries == null || !databaseDataDto.Entries.Any())
            {
                return;
            }
            foreach (var entry in databaseDataDto.Entries)
            {
                var entryTextParts = new List<string>();

                if (entry.OwnerBot != null)
                {
                    entryTextParts.Add($"Entry Owner Bot: {entry.OwnerBot.BotProfileName}");
                }

                if (entry.OwnerUser != null)
                {
                    entryTextParts.Add($"Entry Owner User: {entry.OwnerUser.ProfileName}");
                }

                if (!string.IsNullOrEmpty(entry.Context))
                {
                    entryTextParts.Add($"Entry Context: {entry.Context}");
                }

                if (entryTextParts.Any())
                {
                    mainBody.Contents.Add(new Content(new List<Part> { new Part(string.Join(" | ", entryTextParts)) }));
                }

                if (entry.Post != null)
                {
                    var postTextParts = new List<string>();

                    if (!string.IsNullOrEmpty(entry.Post.Title))
                    {
                        postTextParts.Add($"Post Title: {entry.Post.Title}");
                    }

                    if (!string.IsNullOrEmpty(entry.Post.Context))
                    {
                        postTextParts.Add($"Post Context: {entry.Post.Context}");
                    }

                    if (postTextParts.Any())
                    {
                        mainBody.Contents.Add(new Content(new List<Part> { new Part(string.Join(" | ", postTextParts)) }));
                    }
                }
            }
        }


        public void AddFollowContext_ToRequest(DatabaseDataDto databaseDataDto, MainBody mainBody)
        {
            if ((databaseDataDto.Users == null || !databaseDataDto.Users.Any()) && (databaseDataDto.Bots == null || !databaseDataDto.Bots.Any()))
            {
                return;
            }
            if (databaseDataDto.Users != null)
            {
                foreach (var user in databaseDataDto.Users)
                {
                    var partText = $"User Id: {user.Id}" +
                                   (!string.IsNullOrEmpty(user.ProfileName) ? $" | Profile Name: {user.ProfileName}" : "") +
                                   ($" | Interests: {user.Interests}");

                    mainBody.Contents.Add(new Content(new List<Part> { new Part(partText) }));
                }
            }

            if (databaseDataDto.Bots != null)
            {
                foreach (var bot in databaseDataDto.Bots)
                {
                    var partText = $"Bot Id: {bot.Id}" +
                                   (!string.IsNullOrEmpty(bot.BotProfileName) ? $" | Bot Profile Name: {bot.BotProfileName}" : "") +
                                   ($" | Interests: {bot.Interests}");

                    mainBody.Contents.Add(new Content(new List<Part> { new Part(partText) }));
                }
            }
        }

        public void AddNewsContext_ToRequest(DatabaseDataDto databaseDataDto, MainBody mainBody)
        {
            if (databaseDataDto.News == null || !databaseDataDto.News.Any())
            {
                return;
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
        }

        public void AddMemoryContext_ToRequest(DatabaseDataDto databaseDataDto, MainBody mainBody)
        {
            if (databaseDataDto.BotMemoryLogs == null || !databaseDataDto.BotMemoryLogs.Any())
            {
                return;
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
        }

        public void SetOperationInstructions_WithRequest(DatabaseDataDto databaseDataDto, MainBody mainBody)
        {

            if (databaseDataDto.ActivityType == BotActivityType.BotCreatedEntry)
            {
                mainBody.SystemInstruction.parts.Add(new Part("Choose between posts"));
                mainBody.SystemInstruction.parts.Add(new Part("Consider post topics"));
                mainBody.SystemInstruction.parts.Add(new Part("Create entry response for that post"));
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
                mainBody.SystemInstruction.parts.Add(new Part("Create entry response for that entry"));
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



        }

        public void SetPersonalityInstructions_WithRequest(Bot bot, DatabaseDataDto databaseDataDto, MainBody mainBody)
        {
            mainBody.SystemInstruction.parts.Add(new Part($"Your Personality: {bot.BotPersonality}"));
            if (databaseDataDto.BotMemoryLogs != null)
            {
                mainBody.SystemInstruction.parts.Add(new Part("Consider your memories while performing the operation"));
            }
            mainBody.SystemInstruction.parts.Add(new Part($"{bot.Instructions}"));
        }


        public void ChangeIntelligenceByGrade_WithRequest(Bot bot, MainBody mainBody)
        {
            if (bot.BotGrade == BotGrades.Low)
            {
                mainBody.GenerationConfig.Temperature = 0.9;
                mainBody.GenerationConfig.TpNumber = 0.8;
                mainBody.GenerationConfig.TkNumber = 40;
            }
            else if (bot.BotGrade == BotGrades.Medium)
            {
                mainBody.GenerationConfig.Temperature = 0.7;
                mainBody.GenerationConfig.TpNumber = 0.9;
                mainBody.GenerationConfig.TkNumber = 50;
            }
            else if (bot.BotGrade == BotGrades.High)
            {
                mainBody.GenerationConfig.Temperature = 0.5;
                mainBody.GenerationConfig.TpNumber = 0.95;
                mainBody.GenerationConfig.TkNumber = 60;
            }
        }


        public void SetPricing_WithRequest(Bot bot, MainBody mainBody)
        {
        }






    }
}
