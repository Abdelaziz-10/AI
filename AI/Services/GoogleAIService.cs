using GestionDesPresences.AI.Configuration;
using GestionDesPresences.AI.DTOs;
using GestionDesPresences.AI.Interfaces;
using GestionDesPresences.AI.Models;
using GestionDesPresences.Services;
using Microsoft.Extensions.Options;

namespace GestionDesPresences.AI.Services
{
    public class GoogleAIService : IGoogleAIService
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleAIOptions _options;
        private readonly ILogger<GoogleAIService> _logger;

        public GoogleAIService(
        HttpClient httpClient,
        IOptions<GoogleAIOptions> options,
        ILogger<GoogleAIService> logger)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<GeminiResult> GenerateAsync(string systemPrompt, string userPrompt)
        {
            try
            {


                var apiKey =
                    _options.ApiKey;

                var url =
                    $"https://generativelanguage.googleapis.com/v1beta/models/{_options.Model}:generateContent?key={apiKey}";

                var request = new GeminiRequest
                {
                    Contents =
                {
                    new GeminiContent
                    {
                        Parts =
                        {
                            new GeminiPart
                            {
                                Text = systemPrompt
                            },
                            new GeminiPart
                            {
                                Text = userPrompt
                            }
                        }
                    }
                }

                };
                _logger.LogInformation("Sending request to Gemini...");

                var response =
                    await _httpClient.PostAsJsonAsync(
                        url,
                        request);

                response.EnsureSuccessStatusCode();


                var json =
                    await response.Content.ReadFromJsonAsync<DTOs.GeminiResponse>();
                if (json == null)
                {
                    throw new Exception("Gemini returned no response.");
                }
                var text =
                    json.Candidates
                        .First()
                            .Content
                                .Parts
                                    .First()
                                        .Text;
                _logger.LogInformation("Gemini response received.");

                return new GeminiResult
                {
                    Success = true,
                    Text = text
                };
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling Google AI.");

                return new GeminiResult
                {
                    Success = false,
                    Error = "The AI service is currently unavailable."
                };
            }
        }
    }
}

