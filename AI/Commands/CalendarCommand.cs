using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Pipeline;

namespace GestionDesPresences.AI.Commands
{
    public class CalendarCommand : AICommandBase
    {
        public override IntentType Intent
    => IntentType.Calendar;

        public override async Task<AIResponse> ExecuteAsync(AIExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
