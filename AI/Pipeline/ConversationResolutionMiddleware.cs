using GestionDesPresences.AI.Mappers;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Resolvers;

namespace GestionDesPresences.AI.Pipeline
{
    public class ConversationResolutionMiddleware : IAICommandMiddleware
    {
        private readonly IEntityResolver _resolver;

        public ConversationResolutionMiddleware(
            IEntityResolver resolver)
        {
            _resolver = resolver;
        }

        public async Task<AIResponse> InvokeAsync(
        AIExecutionContext context,
        Func<Task<AIResponse>> next)
        {
            if (context.Employee == null &&
                context.Conversation?.LastEmployeeId != null)
            {
                var employee =
                 await _resolver.ResolveEmployeeAsync(
                     context.Conversation.LastEmployeeId.Value);

                context.Employee =
                await _resolver.ResolveEmployeeAsync(
                    context.Intent.EmployeeName);
            }

            return await next();
        }
    }
}
