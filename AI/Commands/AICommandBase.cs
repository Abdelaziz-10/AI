using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Interfaces;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Pipeline;

namespace GestionDesPresences.AI.Commands
{
    public abstract class AICommandBase : IAICommand
    {
        public abstract IntentType Intent { get; }

        public abstract Task<AIResponse> ExecuteAsync(AIExecutionContext context);

        //public bool CanExecute(string prompt)
        //{
        //    if (string.IsNullOrWhiteSpace(prompt))
        //        return false;

        //    prompt = prompt.ToLowerInvariant();

        //    return Keywords.Any(keyword =>
        //        prompt.Contains(keyword.ToLowerInvariant()));
        //}

        // abstract Task<AIResponse> ExecuteAsync(string prompt);
    }
}