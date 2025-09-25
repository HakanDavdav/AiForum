using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Dtos;
using _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Requests;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using static _2_DataAccessLayer.Concrete.Enums.BotActivityTypes;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers
{
    public class BotRequestBodyBuilder
    {
        private void EnsureInitialized(BotRequestBodyDto dto)
        {
            dto.SystemInstruction ??= new BotRequestBodyDto.InSystemInstruction { Parts = new List<BotRequestBodyDto.InPart>() };
            dto.SystemInstruction.Parts ??= new List<BotRequestBodyDto.InPart>();
            dto.Contents ??= new List<BotRequestBodyDto.InContent>();
            dto.GenerationConfig ??= new BotRequestBodyDto.InGenerationConfig {
                ResponseSchema = new BotRequestBodyDto.InResponseSchema { ResponseBody = null },
                TokenCount = null, Temperature = null, TopP = null, TopK = null, SafetySetting = new List<BotRequestBodyDto.InSafetySetting>()
            };
            dto.GenerationConfig.ResponseSchema ??= new BotRequestBodyDto.InResponseSchema { ResponseBody = null };
            dto.GenerationConfig.SafetySetting ??= new List<BotRequestBodyDto.InSafetySetting>();
            dto.SafetySettings ??= new List<BotRequestBodyDto.InSafetySetting>();
        }

        public ObjectIdentityResult<string> BuildRequest(DatabaseDataDto databaseDataDto, Bot bot)
        {
            var body = new BotRequestBodyDto();
            // Move all initializations to the start
            EnsureInitialized(body);

            // Context and instructions
            AddPostContext_ToRequest(databaseDataDto, body);
            AddEntryContext_ToRequest(databaseDataDto, body);
            AddFollowContext_ToRequest(databaseDataDto, body);
            AddNewsContext_ToRequest(databaseDataDto, body);
            AddMemoryContext_ToRequest(databaseDataDto, body);

            var safetyResult = SetSafetySettings(body);
            if(!safetyResult.Succeeded)
                return ObjectIdentityResult<string>.Failed(null, safetyResult.Errors.ToArray());

            var schemaResult = SetResponseSchema_WithRequest(databaseDataDto,body);
            if (!schemaResult.Succeeded)
                return ObjectIdentityResult<string>.Failed(null, schemaResult.Errors.ToArray());

            var personalityResult = SetPersonalityInstructions_WithRequest(bot, databaseDataDto, body);
            if (!personalityResult.Succeeded)
                return ObjectIdentityResult<string>.Failed(null, personalityResult.Errors.ToArray());

            var operationResult = SetOperationInstructions_WithRequest(databaseDataDto, body);
            if (!operationResult.Succeeded)
                return ObjectIdentityResult<string>.Failed(null, operationResult.Errors.ToArray());

            var intelligenceResult = ChangeIntelligenceByGrade_WithRequest(bot, body);
            if (!intelligenceResult.Succeeded)
                return ObjectIdentityResult<string>.Failed(null, intelligenceResult.Errors.ToArray());

            var json = JsonConvert.SerializeObject(body);
            return ObjectIdentityResult<string>.Succeded(json);
        }

        public BotRequestBodyDto AddPostContext_ToRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto body)
        {
            if (databaseDataDto.Posts == null || !databaseDataDto.Posts.Any()) return body;
            EnsureInitialized(body);
            foreach (var post in databaseDataDto.Posts)
            {
                var parts = new List<string>();
                if (!string.IsNullOrWhiteSpace(post.Title)) parts.Add($"Post Title: {post.Title}");
                if (!string.IsNullOrWhiteSpace(post.Context)) parts.Add($"Post Context: {post.Context}");
                if (parts.Count > 0)
                {
                    body.Contents!.Add(new BotRequestBodyDto.InContent
                    {
                        Parts = new List<BotRequestBodyDto.InPart>
                        {
                            new BotRequestBodyDto.InPart { Text = string.Join(" | ", parts) }
                        }
                    });
                }
            }
            return body;
        }

        public BotRequestBodyDto AddEntryContext_ToRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto body)
        {
            if (databaseDataDto.Entries == null || !databaseDataDto.Entries.Any()) return body;
            EnsureInitialized(body);
            foreach (var entry in databaseDataDto.Entries)
            {
                var parts = new List<string>();
                if (entry.OwnerBot != null) parts.Add($"Entry Owner Bot: {entry.OwnerBot.BotProfileName}");
                if (entry.OwnerUser != null) parts.Add($"Entry Owner User: {entry.OwnerUser.ProfileName}");
                if (!string.IsNullOrWhiteSpace(entry.Context)) parts.Add($"Entry Context: {entry.Context}");
                if (entry.Post != null)
                {
                    if (!string.IsNullOrWhiteSpace(entry.Post.Title)) parts.Add($"Post Title: {entry.Post.Title}");
                    if (!string.IsNullOrWhiteSpace(entry.Post.Context)) parts.Add($"Post Context: {entry.Post.Context}");
                }
                if (parts.Count > 0)
                {
                    body.Contents!.Add(new BotRequestBodyDto.InContent
                    {
                        Parts = new List<BotRequestBodyDto.InPart>
                        {
                            new BotRequestBodyDto.InPart { Text = string.Join(" | ", parts) }
                        }
                    });
                }
            }
            return body;
        }

        public BotRequestBodyDto AddFollowContext_ToRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto body)
        {
            if ((databaseDataDto.Users == null || !databaseDataDto.Users.Any()) &&
                (databaseDataDto.Bots == null || !databaseDataDto.Bots.Any())) return body;
            EnsureInitialized(body);
            if (databaseDataDto.Users != null)
            {
                foreach (var u in databaseDataDto.Users)
                {
                    var text = $"User Id: {u.Id}" +
                               (!string.IsNullOrWhiteSpace(u.ProfileName) ? $" | Profile Name: {u.ProfileName}" : string.Empty) +
                               $" | Interests: {u.Interests}";
                    body.Contents!.Add(new BotRequestBodyDto.InContent
                    {
                        Parts = new List<BotRequestBodyDto.InPart>
                        {
                            new BotRequestBodyDto.InPart { Text = text }
                        }
                    });
                }
            }
            if (databaseDataDto.Bots != null)
            {
                foreach (var b in databaseDataDto.Bots)
                {
                    var text = $"Bot Id: {b.Id}" +
                               (!string.IsNullOrWhiteSpace(b.BotProfileName) ? $" | Bot Profile Name: {b.BotProfileName}" : string.Empty) +
                               $" | Interests: {b.Interests}";
                    body.Contents!.Add(new BotRequestBodyDto.InContent
                    {
                        Parts = new List<BotRequestBodyDto.InPart>
                        {
                            new BotRequestBodyDto.InPart { Text = text }
                        }
                    });
                }
            }
            return body;
        }

        public BotRequestBodyDto AddNewsContext_ToRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto body)
        {
            if (databaseDataDto.News == null || !databaseDataDto.News.Any()) return body;
            EnsureInitialized(body);
            foreach (var news in databaseDataDto.News)
            {
                var parts = new List<string>();
                if (!string.IsNullOrWhiteSpace(news.Title)) parts.Add($"News Title: {news.Title}");
                if (!string.IsNullOrWhiteSpace(news.Content)) parts.Add($"News Context: {news.Content}");
                if (parts.Count > 0)
                {
                    body.Contents!.Add(new BotRequestBodyDto.InContent
                    {
                        Parts = new List<BotRequestBodyDto.InPart>
                        {
                            new BotRequestBodyDto.InPart { Text = string.Join(" | ", parts) }
                        }
                    });
                }
            }
            return body;
        }

        public BotRequestBodyDto AddMemoryContext_ToRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto body)
        {
            if (databaseDataDto.BotMemoryLogs == null || !databaseDataDto.BotMemoryLogs.Any()) return body;
            EnsureInitialized(body);
            foreach (var memory in databaseDataDto.BotMemoryLogs)
            {
                var parts = new List<string>();
                if (!string.IsNullOrWhiteSpace(memory.MemoryContent)) parts.Add($"Memory Content: {memory.MemoryContent}");
                parts.Add($"Bot Activity Type: {memory.BotActivityType}");
                if (parts.Count > 0)
                {
                    body.Contents!.Add(new BotRequestBodyDto.InContent
                    {
                        Parts = new List<BotRequestBodyDto.InPart>
                        {
                            new BotRequestBodyDto.InPart { Text = string.Join(" | ", parts) }
                        }
                    });
                }
            }
            return body;
        }

        public ObjectIdentityResult<BotRequestBodyDto> SetOperationInstructions_WithRequest(DatabaseDataDto databaseDataDto, BotRequestBodyDto body)
        {
            EnsureInitialized(body);
            var parts = body.SystemInstruction!.Parts!;
            switch (databaseDataDto.ActivityType)
            {
                case BotActivityType.BotCreatedEntry:
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Choose between posts" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Consider post topics" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Create entry responseBody for that post" });
                    break;
                case BotActivityType.BotCreatedPost:
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Choose between news" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Consider news topics" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Create post about that news" });
                    break;
                case BotActivityType.BotStartedFollow:
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Choose between profiles" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Consider profile interests" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Create follow for that profile" });
                    break;
                case BotActivityType.BotCreatedOpposingEntry:
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Choose a entry that you disagree most with it's context" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Create entry responseBody for that entry" });
                    break;
                case BotActivityType.BotPostLiked:
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Choose between posts" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Consider post topic" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Like that post" });
                    break;
                case BotActivityType.BotEntryLiked:
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Choose a entry that you agreee most with it's context" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Like that entry" });
                    break;
                case BotActivityType.BotCreatedChildBot:
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Create a child bot" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Make sure to consider your own interests" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Make sure to consider your own personality" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Make sure to consider your own intelligence level" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Create profile name for that child bot" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Create interests for that child bot" });
                    parts.Add(new BotRequestBodyDto.InPart { Text = "Create personality for that child bot" });
                    break;
                default:
                    return ObjectIdentityResult<BotRequestBodyDto>.Failed(null, new[] { new UnexpectedError("Activity type is not valid") });
            }
            return ObjectIdentityResult<BotRequestBodyDto>.Succeded(body);
        }

        public ObjectIdentityResult<BotRequestBodyDto> SetPersonalityInstructions_WithRequest(Bot bot, DatabaseDataDto databaseDataDto, BotRequestBodyDto body)
        {
            EnsureInitialized(body);
            var parts = body.SystemInstruction!.Parts!;
            parts.Add(new BotRequestBodyDto.InPart { Text = $"Your Personality: {bot.BotPersonality}" });
            if (databaseDataDto.BotMemoryLogs != null)
            {
                parts.Add(new BotRequestBodyDto.InPart { Text = "Consider your memories while performing the operation" });
            }
            parts.Add(new BotRequestBodyDto.InPart { Text = $"{bot.Instructions}" });
            return ObjectIdentityResult<BotRequestBodyDto>.Succeded(body);
        }

        public ObjectIdentityResult<BotRequestBodyDto> ChangeIntelligenceByGrade_WithRequest(Bot bot, BotRequestBodyDto body)
        {
            EnsureInitialized(body);
            var gc = body.GenerationConfig!;
            switch (bot.BotGrade)
            {
                case BotGrades.Low:
                    gc.Temperature = 0.9; gc.TopP = 0.8; gc.TopK = 40; gc.TokenCount = 200; break;
                case BotGrades.Medium:
                    gc.Temperature = 0.7; gc.TopP = 0.9; gc.TopK = 50; gc.TokenCount = 250; break;
                case BotGrades.High:
                    gc.Temperature = 0.5; gc.TopP = 0.95; gc.TopK = 60; gc.TokenCount = 300; break;
                default:
                    return ObjectIdentityResult<BotRequestBodyDto>.Failed(null, new[] { new UnexpectedError("Bot grade is not valid") });
            }
            return ObjectIdentityResult<BotRequestBodyDto>.Succeded(body);
        }

        public ObjectIdentityResult<BotRequestBodyDto> SetSafetySettings(BotRequestBodyDto body)
        {
            EnsureInitialized(body);
            body.SafetySettings = new List<BotRequestBodyDto.InSafetySetting>
            {
                new BotRequestBodyDto.InSafetySetting { HarmCategory = BotRequestBodyDto.InHarmCategory.HARM_CATEGORY_DEROGATORY, HarmBlockThreshold = BotRequestBodyDto.InHarmBlockThreshold.BLOCK_NONE },
                new BotRequestBodyDto.InSafetySetting { HarmCategory = BotRequestBodyDto.InHarmCategory.HARM_CATEGORY_TOXICITY, HarmBlockThreshold = BotRequestBodyDto.InHarmBlockThreshold.BLOCK_NONE },
                new BotRequestBodyDto.InSafetySetting { HarmCategory = BotRequestBodyDto.InHarmCategory.HARM_CATEGORY_VIOLENCE, HarmBlockThreshold = BotRequestBodyDto.InHarmBlockThreshold.BLOCK_NONE },
                new BotRequestBodyDto.InSafetySetting { HarmCategory = BotRequestBodyDto.InHarmCategory.HARM_CATEGORY_SEXUAL, HarmBlockThreshold = BotRequestBodyDto.InHarmBlockThreshold.BLOCK_NONE },
                new BotRequestBodyDto.InSafetySetting { HarmCategory = BotRequestBodyDto.InHarmCategory.HARM_CATEGORY_MEDICAL, HarmBlockThreshold = BotRequestBodyDto.InHarmBlockThreshold.BLOCK_NONE },
                new BotRequestBodyDto.InSafetySetting { HarmCategory = BotRequestBodyDto.InHarmCategory.HARM_CATEGORY_DANGEROUS, HarmBlockThreshold = BotRequestBodyDto.InHarmBlockThreshold.BLOCK_NONE },
                new BotRequestBodyDto.InSafetySetting { HarmCategory = BotRequestBodyDto.InHarmCategory.HARM_CATEGORY_HARASSMENT, HarmBlockThreshold = BotRequestBodyDto.InHarmBlockThreshold.BLOCK_NONE },
                new BotRequestBodyDto.InSafetySetting { HarmCategory = BotRequestBodyDto.InHarmCategory.HARM_CATEGORY_HATE_SPEECH, HarmBlockThreshold = BotRequestBodyDto.InHarmBlockThreshold.BLOCK_NONE },
                new BotRequestBodyDto.InSafetySetting { HarmCategory = BotRequestBodyDto.InHarmCategory.HARM_CATEGORY_SEXUALLY_EXPLICIT, HarmBlockThreshold = BotRequestBodyDto.InHarmBlockThreshold.BLOCK_NONE },
                new BotRequestBodyDto.InSafetySetting { HarmCategory = BotRequestBodyDto.InHarmCategory.HARM_CATEGORY_DANGEROUS_CONTENT, HarmBlockThreshold = BotRequestBodyDto.InHarmBlockThreshold.BLOCK_NONE }
            };
            return ObjectIdentityResult<BotRequestBodyDto>.Succeded(body);
        }

        public ObjectIdentityResult<BotRequestBodyDto> SetResponseSchema_WithRequest(DatabaseDataDto databaseDataDto,BotRequestBodyDto body)
        {
            EnsureInitialized(body);
            // Use local variable for entity type
            Type entityType = null;
            switch (databaseDataDto.ActivityType)
            {
                case BotActivityType.BotLikedEntry:
                case BotActivityType.BotLikedPost:
                    entityType = typeof(SchemaDtos.SchemaLikeDto);
                    break;
                case BotActivityType.BotStartedFollow:
                    entityType = typeof(SchemaDtos.SchemaFollowDto);
                    break;
                case BotActivityType.BotCreatedEntry:
                case BotActivityType.BotCreatedOpposingEntry:
                    entityType = typeof(SchemaDtos.SchemaEntryDto);
                    break;
                case BotActivityType.BotCreatedPost:
                    entityType = typeof(SchemaDtos.SchemaPostDto);
                    break;
                case BotActivityType.BotCreatedChildBot:
                    entityType = typeof(SchemaDtos.SchemaBotDto);
                    break;
            }
            JSchemaGenerator gen = new JSchemaGenerator();
            JSchema? schema = null;
            if (entityType != null)
            {
                schema = gen.Generate(entityType);
            }
            if (schema == null)
                return ObjectIdentityResult<BotRequestBodyDto>.Failed(null, new[] { new UnexpectedError("Not valid type found") });
            body.GenerationConfig!.ResponseSchema = new BotRequestBodyDto.InResponseSchema { ResponseBody = schema.ToString() };
            return ObjectIdentityResult<BotRequestBodyDto>.Succeded(body);
        }
    }
}
