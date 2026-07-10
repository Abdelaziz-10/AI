using GestionDesPresences.AI.Factories;
using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Pipeline
{
    public class AICommandPipeline
    {
        private readonly IEnumerable<IAICommandMiddleware> _middlewares;

        public AICommandPipeline(
            IEnumerable<IAICommandMiddleware> middlewares)
        {
            _middlewares = middlewares;
        }

        public async Task<AIResponse> ExecuteAsync(
        AIExecutionContext context,
        Func<Task<AIResponse>> handler)
        {
            Func<Task<AIResponse>> pipeline = handler;

            foreach (var middleware in _middlewares.Reverse())
            {
                var next = pipeline;

                pipeline = () => middleware.InvokeAsync(context, next);
            }

            //var response = await pipeline();
            try
            {
                var response = await pipeline();

                context.Response = response;

                return response;
            }
            catch (Exception ex)
            {
                return AIResponseFactory.Error(ex.Message);
            }

           
        }
    }
}
