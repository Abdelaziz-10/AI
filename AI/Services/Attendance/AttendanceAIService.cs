using GestionDesPresences.AI.Factories;
using GestionDesPresences.AI.Models;
using GestionDesPresences.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace GestionDesPresences.AI.Services.Attendance
{
    public class AttendanceAIService : IAttendanceAIService
    {
        private readonly ApplicationDbContext _context;

        public AttendanceAIService(
            ApplicationDbContext context)
        {
            _context = context;
        }

      
        public async Task<AIResponse> GetAbsentTodayAsync( )
        {
            var employees = await _context.Presences
                .AsNoTracking()
                .Where(p =>
                    p.Date.Date == DateTime.Today &&
                    !p.EstPresent)
                .Select(p => p.Collaborateur.Nom)
                .ToListAsync();

            if (!employees.Any())
            {
                return new AIResponse
                {
                    Success = true,
                    Message = "🎉 No employees are absent today."
                };
            }

            var builder = new StringBuilder();

            builder.AppendLine("❌ Employees absent today:");
            builder.AppendLine();

            foreach (var employee in employees)
            {
                builder.AppendLine($"• {employee}");
            }

            return new AIResponse
            {
                Success = true,
                Message = builder.ToString()
            };
        }

        public Task<AIResponse> GetLateEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AIResponse> GetMissingCheckoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AIResponse> GetPresentTodayAsync( )
        {
            var employees = await _context.Presences
                .AsNoTracking()
                .Where(p =>
                    p.Date.Date == DateTime.Today &&
                    p.EstPresent)
                .Select(p => p.Collaborateur.Nom)
                .ToListAsync();

            if (!employees.Any())
            {
                return new AIResponse
                {
                    Success = true,
                    Message = "🎉 No employees are present today."
                };
            }

            var builder = new StringBuilder();

            builder.AppendLine("✅ Employees present today:");
            builder.AppendLine();

            foreach (var employee in employees)
            {
                builder.AppendLine($"• {employee}");
            }

            return new AIResponse
            {
                Success = true,
                Message = builder.ToString()
            };
        }

        public async Task<AIResponse> GetTodaySummaryAsync()
        {
            var total = await _context.Presences
                .CountAsync(x => x.Date.Date == DateTime.Today);

            var present = await _context.Presences
                .CountAsync(x =>
                    x.Date.Date == DateTime.Today &&
                    x.EstPresent);

            var absent = total - present;

            return AIResponseFactory.Success(
                        $"""
                📊 Attendance Summary

                Present : {present}

                Absent : {absent}

                Total : {total}
                """);
        }
    }
}
