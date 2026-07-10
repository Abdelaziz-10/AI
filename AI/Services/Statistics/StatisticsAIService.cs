using GestionDesPresences.AI.Models;
using GestionDesPresences.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace GestionDesPresences.AI.Services.Statistics
{
    public class StatisticsAIService : IStatisticsAIService
    {
        private readonly ApplicationDbContext _context;
        public StatisticsAIService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StatisticsResult> GetMonthlyStatisticsAsync(int month, int year)
        {
            var presences = await _context.Presences
                .Where(x =>
                    x.Date.Month == month &&
                    x.Date.Year == year)
                .ToListAsync();
            int total ;
            var totalEmployees = await _context.Collaborateurs.CountAsync();
            //var totalPresent = presences.Count;
            //var totalAbsent = totalEmployees - totalPresent;
            var totalPresent = presences.Count(p => p.EstPresent);
            var totalAbsent = presences.Count(p => !p.EstPresent);
            total = totalPresent + totalAbsent;
            // Step 2 — Calculate the previous month's statistics
            var previousMonth = month == 1 ? 12 : month - 1;
            var previousYear = month == 1 ? year - 1 : year;
            var attendanceRate = total == 0
                    ? 0
                    : Math.Round((double)totalPresent / total * 100, 2);
            var previous = await _context.Presences
                .Where(x =>
                    x.Date.Month == previousMonth &&
                    x.Date.Year == previousYear)
                .ToListAsync();

            double previousRate =
                previous.Count == 0
                    ? 0
                    : previous.Count(x => x.EstPresent) * 100.0 / previous.Count;
            double difference =
                attendanceRate - previousRate;

            string insight;

            if (difference > 0)
            {
                insight =
                    $"Attendance improved by {difference:F1}% compared to last month.";
            }
            else if (difference < 0)
            {
                insight =
                    $"Attendance decreased by {Math.Abs(difference):F1}% compared to last month.";
            }
            else
            {
                insight =
                    "Attendance remained stable compared to last month.";
            }
            return new StatisticsResult
            {
                Month = month,
                Year = year,

                TotalEmployees = totalEmployees,

                TotalPresent = totalPresent,

                TotalAbsent = totalAbsent,

                AttendanceRate = attendanceRate,

                PreviousMonthRate = previousRate,

                Difference = difference,

                Insight = insight,

                DashboardUrl = "/Admin/Dashboard",

                PdfUrl = $"/Presences/ExportToPdf?month={month}&year={year}",

                ExcelUrl = $"/Presences/ExportToExcel?month={month}&year={year}"
                //MonthlyTrend = 
            };
        }
    }
}
