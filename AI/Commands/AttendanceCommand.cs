using GestionDesPresences.AI.Factories;
using GestionDesPresences.AI.Helpers;
using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Interfaces;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Pipeline;
using GestionDesPresences.AI.Services.Attendance;
using GestionDesPresences.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace GestionDesPresences.AI.Commands
{
    public class AttendanceCommand : AICommandBase
    {
        private readonly IAttendanceAIService _attendance;

        public AttendanceCommand(
            IAttendanceAIService attendance)
        {
            _attendance = attendance;
        }

        public override IntentType Intent
            => IntentType.Attendance;

        //public enum AttendanceQueryType
        //{
        //    Summary,
        //    Present,
        //    Absent
        //}
        //public AttendanceQueryType AttendanceQuery { get; set; }
        //= AttendanceQueryType.Summary;
        public override Task<AIResponse> ExecuteAsync(
    AIExecutionContext context)
        {
            return context.Intent.AttendanceAction switch
            {
                AttendanceAction.Absent =>
                    _attendance.GetAbsentTodayAsync(),

                AttendanceAction.Present =>
                    _attendance.GetPresentTodayAsync(),

                AttendanceAction.Late =>
                    _attendance.GetLateEmployeesAsync(),

                AttendanceAction.MissingCheckout =>
                    _attendance.GetMissingCheckoutAsync(),

                _ =>
                    _attendance.GetTodaySummaryAsync()
            };
        }
    }
    // Instead of implementing IAICommand directly:
    //public class AttendanceCommand : AICommandBase
    //{
    //    private readonly ApplicationDbContext _context;

    //    public AttendanceCommand(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    // Create the first command
    //    //For now it returns a simple message. Later, we'll inject ApplicationDbContext and query the database.
    //    //public override string Name => "Attendance";
    //    public override IntentType Intent => IntentType.Attendance;

    //    //protected override List<string> Keywords => new()
    //    //{
    //    //   "attendance",

    //    //    "absent",

    //    //    "absence",

    //    //    "present",

    //    //    "today",

    //    //    "late",

    //    //    "missing",

    //    //    "didn't come",

    //    //    "not present"
    //    //};

    //    //private bool IsAbsentToday(string prompt)
    //    //{
    //    //    string[] phrases =
    //    //    {
    //    //        "absent",
    //    //        "missing",
    //    //        "didn't come",
    //    //        "not present",
    //    //        "who is absent",
    //    //        "who didn't come",
    //    //        "absentees"
    //    //    };

    //    //    return phrases.Any(p =>
    //    //        prompt.Contains(p,
    //    //            StringComparison.OrdinalIgnoreCase))
    //    //        &&
    //    //        prompt.Contains("today",
    //    //            StringComparison.OrdinalIgnoreCase);
    //    //}
    //    public enum AttendanceQueryType
    //    {
    //        Summary,
    //        Present,
    //        Absent
    //    }
    //    public AttendanceQueryType AttendanceQuery { get; set; }
    //    = AttendanceQueryType.Summary;
    //    public override async Task<AIResponse> ExecuteAsync(AIExecutionContext context)
    //    {
    //        return context.Intent.AttendanceQuery switch
    //        {
    //            AttendanceQueryType.Absent =>
    //                await GetAbsentEmployeesToday(CancellationToken.None),

    //            AttendanceQueryType.Present =>
    //                await GetPresentEmployeesToday(CancellationToken.None),

    //            _ =>
    //                await GetAttendanceSummaryToday(CancellationToken.None)
    //        };
    //    }

    //    private async Task<AIResponse> GetAttendanceSummaryToday(
    //    CancellationToken cancellationToken)
    //    {
    //        var total = await _context.Presences
    //            .AsNoTracking()
    //            .CountAsync(
    //                p => p.Date.Date == DateTime.Today,
    //                cancellationToken);

    //        var present = await _context.Presences
    //            .AsNoTracking()
    //            .CountAsync(
    //                p => p.Date.Date == DateTime.Today &&
    //                     p.EstPresent,
    //                cancellationToken);

    //        var absent = total - present;

    //        return AIResponseFactory.Success(
    //            $"📊 Attendance today\n\nPresent: {present}\nAbsent: {absent}\nTotal: {total}");
    //    }

    //    private async Task<AIResponse> GetPresentEmployeesToday(CancellationToken cancellationToken)
    //    {
    //        var employees = await _context.Presences
    //            .AsNoTracking()
    //            .Where(p =>
    //                p.Date.Date == DateTime.Today &&
    //                p.EstPresent)
    //            .Select(p => p.Collaborateur.Nom)
    //            .ToListAsync(cancellationToken );

    //        return new AIResponse
    //        {
    //            Success = true,
    //            Message =
    //                $"✅ {employees.Count} employees are present today."
    //        };
    //    }
    //}
}
