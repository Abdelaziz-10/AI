using GestionDesPresences.AI.Mappers;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Resolvers;
using GestionDesPresences.AI.Services.Employees;

namespace GestionDesPresences.AI.Pipeline
{
    public class EntityResolutionMiddleware : IAICommandMiddleware
    {
        private readonly IEntityResolver _resolver;
        private readonly IEmployeeAIService _employeeService;
        public EntityResolutionMiddleware(
            IEntityResolver resolver,
            IEmployeeAIService employeeService)
        {
            _resolver = resolver;
            _employeeService = employeeService;
        }

        public async Task<AIResponse> InvokeAsync(
        AIExecutionContext context,
        Func<Task<AIResponse>> next)
        {
            if (!string.IsNullOrWhiteSpace(context.Intent.EmployeeName))
            {
                context.Employee =
                    await _resolver.ResolveEmployeeAsync(
                        context.Intent.EmployeeName);
            }

            return await next();
        }
    }
}
