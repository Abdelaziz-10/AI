using DocumentFormat.OpenXml.Office2010.Excel;
using GestionDesPresences.AI.Context;
using GestionDesPresences.AI.Factories;
using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Mappers;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Pipeline;
using GestionDesPresences.AI.Resolvers;
using GestionDesPresences.AI.Services.Employees;
using GestionDesPresences.Data;
using GestionDesPresences.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionDesPresences.AI.Commands
{
    public class EmployeeCommand : AICommandBase
    {
        private readonly IEmployeeAIService _employeeService;

        public EmployeeCommand(IEmployeeAIService employeeService)
        {
            _employeeService = employeeService;
        }

        public override IntentType Intent => IntentType.Employee;

        public override async Task<AIResponse> ExecuteAsync(
    AIExecutionContext context)
        {
            if (context.Employee == null)
                return AIResponseFactory.Error("Employee not found.");

            var card =
                await _employeeService.GetEmployeeCardByIdAsync(
                    context.Employee.Id);

            if (card == null)
                return AIResponseFactory.Error("Employee not found.");

            context.Conversation.LastEmployeeId = card.Id;
            context.Conversation.LastEmployeeName = card.Name;

            return AIResponseFactory.Employee(card);
        }
    }
}
/*
  var employee =
                await _context.Collaborateurs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x =>
                        x.Nom.Contains(intent.EmployeeName!));
            var allPresences = await _context.Presences
                .Where(p => p.CollaborateurId == employee.Id)
                .ToListAsync();

            var presentCount = allPresences.Count(p => p.EstPresent);
            var absentCount = allPresences.Count(p => !p.EstPresent);
            var total = presentCount + absentCount;
            
            var monthlyData = allPresences
                .GroupBy(p => new { p.Date.Year, p.Date.Month })
                .Select(g => new MonthlyPresenceStats
                {
                    Label = new DateTime(g.Key.Year, g.Key.Month, 1)
                        .ToString("MMMM yyyy", System.Globalization.CultureInfo.CurrentCulture),

                    Present = g.Count(p => p.EstPresent),

                    Absent = g.Count(p => !p.EstPresent)
                })
                .OrderBy(g => g.Label)
                .ToList();
            if (employee == null) {
                return new AIResponse { Message = "Employee not found." };
            }
            _conversation.RememberEmployee(
                employee.Id,
                employee.Nom);
            return new AIResponse
            {
                Success = true,

                Action = AIAction.ShowEmployee,

                Message = $"Employee {employee.Nom} found.",

                Data = new EmployeeCardResult
                {
                    Id = employee.Id,

                    Name = employee.Nom,

                    Email = employee.Email,

                    Department = employee.Poste,
                    IsPresentToday = employee.Presences?.Any(p => p.Date.Date == DateTime.Today) ?? false,
                    AttendanceRate = total == 0
                    ? 0
                    : Math.Round((double)presentCount / total * 100, 2)
                }
            };
 */