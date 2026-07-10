using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Interfaces
{
    public interface IGoogleAIService
    {
        Task<GeminiResult> GenerateAsync(
        string systemPrompt,
        string userPrompt);
    }
}
