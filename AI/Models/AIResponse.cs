namespace GestionDesPresences.AI.Models
{
    public class AIResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public AIAction Action { get; set; }
        public object? Data { get; set; }

        public bool RequiresConfirmation { get; set; }
    }

    public enum AIAction
    {
        None,

        Download,

        Navigate,

        ShowChart,

        ShowDialog,

        Confirmation,

        RefreshDashboard,
        ShowEmployee,
        ShowStatistics
    }
}
