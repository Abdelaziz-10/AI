using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Interfaces;

namespace GestionDesPresences.AI.Parsers
{
    public class RuleBasedIntentParser : IIntentParser
    {
        public Task<AIIntent> ParseAsync(string prompt)
        {
            prompt = prompt.ToLower();

            var intent = new AIIntent
            {
                OriginalPrompt = prompt
            };

            if (prompt.Contains("report") ||
                prompt.Contains("pdf") ||
                prompt.Contains("download"))
            {
                intent.Intent = IntentType.Report;
            }
            else if (prompt.Contains("absent") ||
                     prompt.Contains("present"))
            {
                intent.Intent = IntentType.Attendance;
            }
            else if (prompt.Contains("statistics") ||
                     prompt.Contains("stats"))
            {
                intent.Intent = IntentType.Statistics;
            }
            else
            {
                intent.Intent = IntentType.Unknown;
            }

            return Task.FromResult(intent);
        }
    }
}