using GestionDesPresences.AI.Helpers;
using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Interfaces;
using GestionDesPresences.AI.Prompts;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace GestionDesPresences.AI.Parsers
{
    public class GeminiIntentParser : IIntentParser
    {
        private readonly IGoogleAIService _googleAI;
        private readonly IntentPrompt _intentPrompt;
        private readonly ILogger<GeminiIntentParser> _logger;

        public GeminiIntentParser(
        IGoogleAIService googleAI,
        IntentPrompt intentPrompt,
        ILogger<GeminiIntentParser> logger)
        {
            _googleAI = googleAI;
            _intentPrompt = intentPrompt;
            _logger = logger;
        }

        public async Task<AIIntent> ParseAsync(string prompt)
        {
            var userPrompt = _intentPrompt.BuildUserPrompt(prompt);

            var result = await _googleAI.GenerateAsync(
                _intentPrompt.SystemPrompt,
                _intentPrompt.BuildUserPrompt(prompt));
            Console.WriteLine(result.Text);
            if (!result.Success)
            {
                _logger.LogError(result.Error);

                return new AIIntent
                {
                    Intent = IntentType.Unknown,
                    OriginalPrompt = prompt
                };
            }

            var json = result.Text;

            // throw new Exception(json);

            //Console.WriteLine(json);
            json = JsonCleaner.Clean(json);
            //Console.WriteLine(json);
            //_logger.LogInformation(json);
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new JsonStringEnumConverter());
                var intent = JsonSerializer.Deserialize<AIIntent>(json, options);

                if (intent == null)
                    throw new Exception("Invalid AI response.");

                intent.OriginalPrompt = prompt;

                return intent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to parse Gemini response.");

                return new AIIntent
                {
                    Intent = IntentType.Unknown,
                    OriginalPrompt = prompt
                };
            }

            // Deserialize later
            //return JsonSerializer.Deserialize<AIIntent>(json)!;
        }
    }
}
