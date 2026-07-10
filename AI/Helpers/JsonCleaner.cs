namespace GestionDesPresences.AI.Helpers
{
    public static class JsonCleaner
    {
        public static string Clean(string text)
        {
            return text
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();
        }
    }
}
