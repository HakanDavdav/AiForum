using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.BotBackgroundService.BotManagers.Requests
{
    public class BotRequestBodyDto
    {
        public enum InHarmCategory
        {
            HARM_CATEGORY_UNSPECIFIED,      // Category is unspecified
            HARM_CATEGORY_DEROGATORY,       // PaLM - Negative or harmful comments targeting identity/protected attribute
            HARM_CATEGORY_TOXICITY,         // PaLM - Rude, disrespectful, or profane content
            HARM_CATEGORY_VIOLENCE,         // PaLM - Violence/gore descriptions
            HARM_CATEGORY_SEXUAL,           // PaLM - Sexual acts or lewd content
            HARM_CATEGORY_MEDICAL,          // PaLM - Unchecked medical advice
            HARM_CATEGORY_DANGEROUS,        // PaLM - Dangerous acts encouragement
            HARM_CATEGORY_HARASSMENT,       // Gemini - Harassment content
            HARM_CATEGORY_HATE_SPEECH,      // Gemini - Hate speech
            HARM_CATEGORY_SEXUALLY_EXPLICIT,// Gemini - Sexually explicit content
            HARM_CATEGORY_DANGEROUS_CONTENT // Gemini - Dangerous content
        }

        public enum InHarmBlockThreshold
        {
            HARM_BLOCK_THRESHOLD_UNSPECIFIED, // Threshold is unspecified
            BLOCK_LOW_AND_ABOVE,              // Blocks LOW and higher probabilities
            BLOCK_MEDIUM_AND_ABOVE,           // Blocks MEDIUM and higher probabilities
            BLOCK_ONLY_HIGH,                  // Blocks HIGH only
            BLOCK_NONE,                       // No blocking, all allowed
            OFF                               // Turns off safety filter entirely
        }

        [JsonProperty("system_instruction")]
        public InSystemInstruction? SystemInstruction { get; set; }

        [JsonProperty("contents")]
        public required List<InContent>? Contents { get; set; }

        [JsonProperty("generationConfig")]
        public InGenerationConfig? GenerationConfig { get; set; }

        [JsonProperty("safetySettings")]
        public List<InSafetySetting>? SafetySettings { get; set; }

        public class InPart
        {
            [JsonProperty("text")]
            public string Text { get; set; }

            public InPart(string text)
            {
                Text = text;
            }
        }

        public class InSystemInstruction
        {
            [JsonProperty("parts")]
            public List<InPart> Parts { get; set; }

            public InSystemInstruction(List<InPart> parts)
            {
                Parts = parts;
            }
        }

        public class InContent
        {
            [JsonProperty("parts")]
            public List<InPart> Parts { get; set; }

            public InContent(List<InPart> parts)
            {
                Parts = parts;
            }
        }

        public class InResponseSchema
        {
            [JsonProperty("responseBody")]
            public string ResponseBody { get; set; }

            public InResponseSchema(string responseBody)
            {
                ResponseBody = responseBody;
            }
        }

        public class InSafetySetting
        {
            [JsonProperty("category")]
            public InHarmCategory HarmCategory { get; set; }

            [JsonProperty("threshold")]
            public InHarmBlockThreshold HarmBlockThreshold { get; set; }

            public InSafetySetting(InHarmCategory harmCategory, InHarmBlockThreshold harmBlockThreshold)
            {
                HarmCategory = harmCategory;
                HarmBlockThreshold = harmBlockThreshold;
            }
        }

        public class InGenerationConfig
        {
            [JsonProperty("responseJsonSchema")]
            public InResponseSchema ResponseSchema { get; set; }

            [JsonProperty("responseMimeType")]
            public string ResponseMimeType { get; set; } = "application/json";

            [JsonProperty("maxOutputTokens")]
            public int TokenCount { get; set; }

            [JsonProperty("temperature")]
            public double Temperature { get; set; }

            [JsonProperty("topP")]
            public double TopP { get; set; }

            [JsonProperty("topK")]
            public double TopK { get; set; }

            [JsonProperty("safetySettings")]
            public List<InSafetySetting> SafetySetting { get; set; }

            public InGenerationConfig(InResponseSchema responseSchema, int tokenCount, double temperature, double topP, double topK)
            {
                ResponseSchema = responseSchema;
                TokenCount = tokenCount;
                Temperature = temperature;
                TopP = topP;
                TopK = topK;
                SafetySetting = new List<InSafetySetting>();
            }
        }
    }
}
