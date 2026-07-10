using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Pipeline;
using GestionDesPresences.AI.Registry;

namespace GestionDesPresences.AI.Workflow
{
    public class AIWorkflowExecutor
    {
        private readonly IAICommandRegistry _registry;
        private readonly AICommandPipeline _pipeline;

        public AIWorkflowExecutor(
            IAICommandRegistry registry,
            AICommandPipeline pipeline)
        {
            _registry = registry;
            _pipeline = pipeline;
        }

        public async Task<List<AIResponse>> ExecuteAsync(
        AIWorkflow workflow,
        AIExecutionContext context)
        {
            List<AIResponse> responses = new();

            foreach (var step in workflow.Steps)
            {
                var command = _registry.Get(step.Intent);
                Console.WriteLine(command == null
                ? "Command NOT found"
                : $"Command: {command.GetType().Name}");
                if (command == null)
                    continue;

                context.Intent = step.IntentData;

                var response =
                    await _pipeline.ExecuteAsync(
                        context,
                        () => command.ExecuteAsync(context));

                responses.Add(response);
            }
            Console.WriteLine($"Responses count = {responses.Count}");
            return responses;
        }
    }
    public class AIWorkflowResult
    {
        public List<AIResponse> Responses { get; set; } = new();

        public bool Success =>
            Responses.All(x => x.Success);

        public AIResponse? Final =>
            Responses.LastOrDefault();
    }
}
