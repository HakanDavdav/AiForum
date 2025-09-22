using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Requests
{
    public class BotResponseDto
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
        public List<InCandidate>? Candidates { get; set; } = new List<InCandidate>();

        [JsonProperty("promptFeedback")]
        public InPromptFeedback? PromptFeedback { get; set; }

        [JsonProperty("usageMetadata")]
        public required InUsageMetadata UsageMetadata { get; set; }

        [JsonProperty("modelVersion")]
        public required string  ModelVersion { get; set; }

        [JsonProperty("responseId")]
        public required string ResponseId { get; set; }

        public class InCandidate
        {
            [JsonProperty("content")]
            public required InContent Content { get; set; }

            [JsonProperty("finishReason")]
            public InFinishReason FinishReason { get; set; }

            [JsonProperty("safetyRatings")]
            public List<InSafetyRating> SafetyRatings { get; set; } = new List<InSafetyRating>();

            [JsonProperty("tokenCount")]
            public int TokenCount { get; set; }

            [JsonProperty("index")]
            public int Index { get; set; }
        }

        public class InContent
        {
            [JsonProperty("parts")]
            public List<InPart> Parts { get; set; } = new List<InPart>();

            [JsonProperty("role")]
            public string? Role { get; set; } // "user" veya "model"
        }

        public class InPart
        {
            [JsonProperty("text")]
            public string Text { get; set; }
            public InPart(string text) { Text = text; }
        }

        public class InSafetyRating
        {
            [JsonProperty("category")]
            public InHarmCategory Category { get; set; }

            [JsonProperty("probability")]
            public InHarmProbability Probability { get; set; }

            [JsonProperty("blocked")]
            public bool Blocked { get; set; }

        }

        public class InPromptFeedback
        {
            [JsonProperty("blockReason")]
            public InBlockReason? BlockReason { get; set; }

            [JsonProperty("safetyRatings")]
            public List<InSafetyRating>? SafetyRatings { get; set; } = new List<InSafetyRating>();
        }

        public class InUsageMetadata
        {
            [JsonProperty("promptTokenCount")]
            public int PromptTokenCount { get; set; }

            [JsonProperty("cachedContentTokenCount")]
            public int CachedContentTokenCount { get; set; }

            [JsonProperty("candidatesTokenCount")]
            public int CandidatesTokenCount { get; set; }

            [JsonProperty("thoughtsTokenCount")]
            public int ThoughtsTokenCount { get; set; }

            [JsonProperty("totalTokenCount")]
            public int TotalTokenCount { get; set; }

            [JsonProperty("promptTokensDetails")]
            public List<InModalityTokenCount> PromptTokensDetails { get; set; } = new List<InModalityTokenCount>();

            [JsonProperty("cacheTokensDetails")]
            public List<InModalityTokenCount> CacheTokensDetails { get; set; } = new List<InModalityTokenCount>();

            [JsonProperty("candidatesTokensDetails")]
            public List<InModalityTokenCount> CandidatesTokensDetails { get; set; } = new List<InModalityTokenCount>();
        }
        public class InModalityTokenCount
        {
            [JsonProperty("modality")]
            public InModality Modality { get; set; }

            [JsonProperty("tokenCount")]
            public int TokenCount { get; set; }
        }
    }
}
