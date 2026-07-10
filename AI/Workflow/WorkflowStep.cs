using GestionDesPresences.AI.Intent;

namespace GestionDesPresences.AI.Workflow
{
    public class WorkflowStep
    {
        public IntentType Intent { get; set; }

        public AIIntent IntentData { get; set; } = new();
    }
}
