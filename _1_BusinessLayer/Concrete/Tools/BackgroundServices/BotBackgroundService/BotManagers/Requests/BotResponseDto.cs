using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Requests
{
    public class BotResponseDto<T> where T : class
    {
        public enum InBlockReason
        {
            BLOCK_REASON_UNSPECIFIED,
            SAFETY,
            OTHER,
            BLOCKLIST,
            PROHIBITED_CONTENT,
            IMAGE_SAFETY
        }

        public enum InFinishReason
        {
            FINISH_REASON_UNSPECIFIED,
            STOP,
            MAX_TOKENS,
            SAFETY,
            RECITATION,
            LANGUAGE,
            OTHER,
            BLOCKLIST,
            PROHIBITED_CONTENT,
            SPII,
            MALFORMED_FUNCTION_CALL,
            IMAGE_SAFETY,
            UNEXPECTED_TOOL_CALL,
            TOO_MANY_TOOL_CALLS
        }

        public enum InHarmCategory
        {
            HARM_CATEGORY_UNSPECIFIED,
            HARM_CATEGORY_DEROGATORY,
            HARM_CATEGORY_TOXICITY,
            HARM_CATEGORY_VIOLENCE,
            HARM_CATEGORY_SEXUAL,
            HARM_CATEGORY_MEDICAL,
            HARM_CATEGORY_DANGEROUS,
            HARM_CATEGORY_HARASSMENT,
            HARM_CATEGORY_HATE_SPEECH,
            HARM_CATEGORY_SEXUALLY_EXPLICIT,
            HARM_CATEGORY_DANGEROUS_CONTENT
        }
        public enum InHarmProbability
        {
            HARM_PROBABILITY_UNSPECIFIED,
            VERY_UNLIKELY,
            UNLIKELY,
            POSSIBLE,
            LIKELY,
            VERY_LIKELY
        }

        public enum InModality
        {
            MODALITY_UNSPECIFIED,
            TEXT,
            IMAGE,
            VIDEO,
            AUDIO,
            DOCUMENT
        }

        [JsonProperty("candidates")]
        public List<InCandidate>? Candidates { get; set; } = null;

        [JsonProperty("promptFeedback")]
        public InPromptFeedback? PromptFeedback { get; set; } = null;

        [JsonProperty("usageMetadata")]
        public InUsageMetadata? UsageMetadata { get; set; } = null;

        [JsonProperty("modelVersion")]
        public string? ModelVersion { get; set; } = null;

        [JsonProperty("responseId")]
        public string? ResponseId { get; set; } = null;

        public class InCandidate
        {
            [JsonProperty("content")]
            public InContent? Content { get; set; } = null;

            [JsonProperty("finishReason")]
            public InFinishReason? FinishReason { get; set; } = null;

            [JsonProperty("safetyRatings")]
            public List<InSafetyRating>? SafetyRatings { get; set; } = null;

            [JsonProperty("tokenCount")]
            public int? TokenCount { get; set; } = null;

            [JsonProperty("index")]
            public int? Index { get; set; } = null;
        }

        public class InContent
        {
            [JsonProperty("parts")]
            public List<InPart>? Parts { get; set; } = null;

            [JsonProperty("role")]
            public string? Role { get; set; } = null;
        }

        public class InPart
        {
            [JsonProperty("text")]
            public string? Text { get; set; }

            [JsonProperty("data")]
            public T? Data { get; set; }

            public InPart() { }
            public InPart(string? text) { Text = text; }
            public InPart(T? data) { Data = data; }
        }

        public class InSafetyRating
        {
            [JsonProperty("category")]
            public InHarmCategory? Category { get; set; } = null;

            [JsonProperty("probability")]
            public InHarmProbability? Probability { get; set; } = null;

            [JsonProperty("blocked")]
            public bool? Blocked { get; set; } = null;
        }

        public class InPromptFeedback
        {
            [JsonProperty("blockReason")]
            public InBlockReason? BlockReason { get; set; } = null;

            [JsonProperty("safetyRatings")]
            public List<InSafetyRating>? SafetyRatings { get; set; } = null;
        }

        public class InUsageMetadata
        {
            [JsonProperty("promptTokenCount")]
            public int? PromptTokenCount { get; set; } = null;

            [JsonProperty("cachedContentTokenCount")]
            public int? CachedContentTokenCount { get; set; } = null;

            [JsonProperty("candidatesTokenCount")]
            public int? CandidatesTokenCount { get; set; } = null;

            [JsonProperty("thoughtsTokenCount")]
            public int? ThoughtsTokenCount { get; set; } = null;

            [JsonProperty("totalTokenCount")]
            public int? TotalTokenCount { get; set; } = null;

            [JsonProperty("promptTokensDetails")]
            public List<InModalityTokenCount>? PromptTokensDetails { get; set; } = null;

            [JsonProperty("cacheTokensDetails")]
            public List<InModalityTokenCount>? CacheTokensDetails { get; set; } = null;

            [JsonProperty("candidatesTokensDetails")]
            public List<InModalityTokenCount>? CandidatesTokensDetails { get; set; } = null;
        }
        public class InModalityTokenCount
        {
            [JsonProperty("modality")]
            public InModality? Modality { get; set; } = null;

            [JsonProperty("tokenCount")]
            public int? TokenCount { get; set; } = null;
        }

        // Custom JSON converter to detect missing properties
        public class MissingPropertyTrackingConverter<TT> : JsonConverter<TT> where TT : class, new()
        {
            public HashSet<string> MissingProperties { get; private set; } = new HashSet<string>();

            public override TT? ReadJson(JsonReader reader, Type objectType, TT? existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                JObject jo = JObject.Load(reader);
                var obj = new TT();
                var props = typeof(TT).GetProperties();
                foreach (var prop in props)
                {
                    var jsonProp = prop.GetCustomAttributes(typeof(JsonPropertyAttribute), true);
                    string jsonName = prop.Name;
                    if (jsonProp.Length > 0)
                        jsonName = ((JsonPropertyAttribute)jsonProp[0]).PropertyName ?? prop.Name;
                    if (!jo.ContainsKey(jsonName))
                        MissingProperties.Add(jsonName);
                }
                serializer.Populate(jo.CreateReader(), obj);
                return obj;
            }

            public override void WriteJson(JsonWriter writer, TT? value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}
