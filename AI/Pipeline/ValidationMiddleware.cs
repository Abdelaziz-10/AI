using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Pipeline
{
    public class ValidationMiddleware : IAICommandMiddleware
    {
        public async Task<AIResponse> InvokeAsync(
            AIExecutionContext context,
            Func<Task<AIResponse>> next)
        {
            Normalize(context.Intent);

            return await next();
        }

        private void Normalize(AIIntent intent)
        {
            if (intent.Month == null)
                intent.Month = DateTime.Today.Month;

            if (intent.Year == null)
                intent.Year = DateTime.Today.Year;

            if (!string.IsNullOrWhiteSpace(intent.EmployeeName))
                intent.EmployeeName = intent.EmployeeName.Trim();

            if (!string.IsNullOrWhiteSpace(intent.Format))
                intent.Format = intent.Format.ToLowerInvariant();
        }
    }
}
