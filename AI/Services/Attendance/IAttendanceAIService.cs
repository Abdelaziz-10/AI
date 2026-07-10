using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Services.Attendance
{
    public interface IAttendanceAIService
    {
        Task<AIResponse> GetAbsentTodayAsync();

        Task<AIResponse> GetPresentTodayAsync();

        Task<AIResponse> GetTodaySummaryAsync();
        Task<AIResponse> GetLateEmployeesAsync();

        Task<AIResponse> GetMissingCheckoutAsync();
    }
}
