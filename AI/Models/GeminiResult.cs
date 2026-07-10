namespace GestionDesPresences.AI.Models
{
    public class GeminiResult
    {
        public bool Success { get; set; }

        public string Text { get; set; } = string.Empty;

        public string? Error { get; set; }

        public int? PromptTokens { get; set; }

        public int? ResponseTokens { get; set; }
    }
}