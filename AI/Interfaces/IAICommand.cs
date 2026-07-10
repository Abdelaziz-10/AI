using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Pipeline;

namespace GestionDesPresences.AI.Interfaces
{
    // This interface defines the contract for AI commands that can be executed based on a given prompt.
    // Every future command (Attendance, Reports, Emails, etc.) will implement this interface.
    //public interface IAICommand
    //{
    //    string Name { get; }

    //    bool CanExecute(string prompt);

    //    Task<AIResponse> ExecuteAsync(string prompt);
    //}
    public interface IAICommand
    {
        IntentType Intent { get; }

        Task<AIResponse> ExecuteAsync(AIExecutionContext context);
    }
}
