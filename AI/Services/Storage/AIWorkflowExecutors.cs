using GestionDesPresences.AI.Interfaces;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Pipeline;
using GestionDesPresences.AI.Registry;
using GestionDesPresences.AI.Workflow;
using Microsoft.Win32;

namespace GestionDesPresences.AI.Services.Storage
{
    public class AIWorkflowExecutors
    {
        private readonly IEnumerable<IAICommand> _commands;
        private readonly IAICommandRegistry _registry;
        private readonly AICommandPipeline _pipeline;

        public AIWorkflowExecutors(
            IEnumerable<IAICommand> commands,
            IAICommandRegistry registry,
            AICommandPipeline pipeline)
        {
            _commands = commands;
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

                if (command == null)
                    continue;

                context.Intent = step.IntentData;

                var response =
                    await _pipeline.ExecuteAsync(
                        context,
                        () => command.ExecuteAsync(context));

                responses.Add(response);
            }

            return responses;
        }
    }
}