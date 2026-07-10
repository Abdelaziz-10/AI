using GestionDesPresences.AI.Models;
using GestionDesPresences.Models;

namespace GestionDesPresences.AI.Mappers
{
    public static class EmployeeMapper
    {

        public static EmployeeCardResult ToCard(
        this Collaborateur employee,
        int presentDays,
        int absentDays,
        bool isPresentToday,
        DateTime? lastAttendance)
        {
            var total = presentDays + absentDays;

            return new EmployeeCardResult
            {
                Id = employee.Id,
                Name = employee.Nom,
                Email = employee.Email,
                Position = employee.Poste,

                PresentDays = presentDays,
                AbsentDays = absentDays,

                IsPresentToday = isPresentToday,

                AttendanceRate = total == 0
                    ? 0
                    : Math.Round(
                        (double)presentDays / total * 100,
                        2),

                LastAttendanceDate = lastAttendance
            };
        }
    }
}
