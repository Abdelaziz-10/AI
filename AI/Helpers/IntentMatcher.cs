namespace GestionDesPresences.AI.Helpers
{
    public static class IntentMatcher
    {
        public static bool Matches(
            string prompt,
            params string[] keywords)
        {
            return keywords.Any(k =>
                prompt.Contains(k,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
}
