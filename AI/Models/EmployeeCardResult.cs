namespace GestionDesPresences.AI.Models
{
    public class EmployeeCardResult
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string? Email { get; set; }

        public string? Department { get; set; }

        public string? Position { get; set; }

        public string? Phone { get; set; }

        public string? PhotoUrl { get; set; }

        public bool IsPresentToday { get; set; }

        public int PresentDays { get; set; }

        public int AbsentDays { get; set; }

        public double AttendanceRate { get; set; }

        public DateTime? LastAttendanceDate { get; set; }
    }
}
