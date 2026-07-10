using GestionDesPresences.AI.Intent;

namespace GestionDesPresences.AI.Workflow
{
    public class WorkflowPlanner
    {
        // Create(AIExecutionContext context) when workflows become multi-step.
        public AIWorkflow Create(AIIntent intent)
        {
            var workflow = new AIWorkflow();

            workflow.Steps.Add(new WorkflowStep
            {
                Intent = intent.Intent,
                IntentData = intent
            });

            return workflow;
        }
    }
}
