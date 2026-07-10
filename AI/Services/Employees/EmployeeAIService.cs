using GestionDesPresences.AI.Mappers;
using GestionDesPresences.AI.Models;
using GestionDesPresences.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionDesPresences.AI.Services.Employees
{
    public class EmployeeAIService : IEmployeeAIService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeAIService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<EmployeeCardResult?> GetEmployeeCardByNameAsync(string? employeeName)
        {
            if (string.IsNullOrWhiteSpace(employeeName))
                return null;

            var employee = await _context.Collaborateurs
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.Nom.Contains(employeeName));
            if (employee == null)
                return null;
            var presences = await _context.Presences
                .Where(x => x.CollaborateurId == employee.Id)
                .ToListAsync();

            double rate = 0;
            var present =
                presences.Count(x => x.EstPresent);

            var absent =
                presences.Count(x => !x.EstPresent);

            var total =
                present + absent;
            if (total > 0)
            {
                rate =
                    present * 100.0 / total;
            }
            

            var today = DateTime.Today;

            bool presentToday =
                presences.Any(x =>
                    x.Date.Date == today &&
                    x.EstPresent);

            var lastAttendance =
                presences
                .OrderByDescending(x => x.Date)
                .FirstOrDefault();

            return employee.ToCard(
                present,
                absent,
                presentToday,
                lastAttendance?.Date);

            //return new EmployeeCardResult
            //{
            //    Id = employee.Id,
            //    Name = employee.Nom,
            //    Email = employee.Email,
            //    Department = employee.Poste,
            //    Position = employee.Poste,
            //    //Phone = employee.Telephone,
            //    //PhotoUrl = employee.PhotoUrl,
            //    IsPresentToday = presentToday,
            //    PresentDays = present,
            //    AbsentDays = absent,
            //    AttendanceRate = total == 0
            //        ? 0
            //        : Math.Round((double)present / total * 100, 2),
            //    LastAttendanceDate = lastAttendance?.Date
            //};
        }

        public async Task<EmployeeCardResult?> GetEmployeeCardByIdAsync(int id)
        {
            var employee = await _context.Collaborateurs
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (employee == null)
                return null;


            var presences = await _context.Presences
                .Where(x => x.CollaborateurId == employee.Id)
                .ToListAsync();


            var present =
                presences.Count(x => x.EstPresent);

            var absent =
                presences.Count(x => !x.EstPresent);


            bool presentToday =
                presences.Any(x =>
                    x.Date.Date == DateTime.Today &&
                    x.EstPresent);


            var lastAttendance =
                presences
                .OrderByDescending(x => x.Date)
                .FirstOrDefault();


            return employee.ToCard(
                present,
                absent,
                presentToday,
                lastAttendance?.Date);
        }
    }
}