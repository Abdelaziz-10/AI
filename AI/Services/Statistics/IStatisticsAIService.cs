using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Services.Statistics
{
    public interface IStatisticsAIService
    {
        Task<StatisticsResult> GetMonthlyStatisticsAsync(
            int month,
            int year);
    }
}
