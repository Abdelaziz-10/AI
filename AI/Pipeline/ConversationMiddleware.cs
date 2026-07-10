using GestionDesPresences.AI.Context;
using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Pipeline
{
    public class ConversationMiddleware : IAICommandMiddleware
    {
        private readonly IAIConversationService _conversation;

        public ConversationMiddleware(
            IAIConversationService conversation)
        {
            _conversation = conversation;
        }

        public async Task<AIResponse> InvokeAsync(
            AIExecutionContext context,
            Func<Task<AIResponse>> next)
        {
            // Load conversation
            context.Conversation = _conversation.Get();

            var response = await next();

            // Save conversation
            _conversation.Update(context.Conversation);

            return response;
        }
    }
}