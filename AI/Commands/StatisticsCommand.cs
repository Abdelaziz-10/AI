using GestionDesPresences.AI.Factories;
using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Pipeline;
using GestionDesPresences.AI.Services.Statistics;

namespace GestionDesPresences.AI.Commands
{
    public class StatisticsCommand : AICommandBase
    {
        private readonly IStatisticsAIService _statisticsService;

        public StatisticsCommand(IStatisticsAIService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        public override IntentType Intent
            => IntentType.Statistics;

        public override async Task<AIResponse> ExecuteAsync(AIExecutionContext context)
        {
            var statistics =
                await _statisticsService
                    .GetMonthlyStatisticsAsync(
                        context.Intent.Month.Value, //?? DateTime.Today.Month,
                        context.Intent.Year.Value);// ?? DateTime.Today.Year);

            return AIResponseFactory.Statistics(statistics);
          
        }
    }
}
