namespace GestionDesPresences.AI.Intent
{
    public class AIIntent
    {
        //public IntentType Intent { get; set; }

        //public Dictionary<string, object> Parameters { get; set; }
        //    = new();
        public IntentType Intent { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public int? EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public string? Format { get; set; }

        public bool NeedsConfirmation { get; set; }
        public string OriginalPrompt { get; set; } = string.Empty;
        //public AttendanceQueryType AttendanceQuery { get; set; }
        //= AttendanceQueryType.Summary;
        public AttendanceAction AttendanceAction { get; set; }
        = AttendanceAction.Summary;

    }
}
