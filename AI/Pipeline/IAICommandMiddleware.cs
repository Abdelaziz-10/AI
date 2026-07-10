using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Pipeline
{
    public interface IAICommandMiddleware
    {
        Task<AIResponse> InvokeAsync(
        AIExecutionContext context,
        Func<Task<AIResponse>> next);
    }
}
