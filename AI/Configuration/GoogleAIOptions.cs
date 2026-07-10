namespace GestionDesPresences.AI.Configuration
{
    public class GoogleAIOptions
    {
        public const string Section = "GoogleAI";

        public string ApiKey { get; set; } = string.Empty;

        public string Model { get; set; } = "gemini-2.5-flash";
        public string BaseUrl { get; set; }
            = "https://generativelanguage.googleapis.com";
    }
}
