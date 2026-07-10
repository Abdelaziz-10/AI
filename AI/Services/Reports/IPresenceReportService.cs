namespace GestionDesPresences.AI.Services.Reports
{
    public interface IPresenceReportService
    {
        Task<byte[]> GenerateMonthlyPdfAsync(int month, int year);

        Task<byte[]> GenerateEmployeeHistoryPdfAsync(
            int employeeId,
            DateTime? selectedDate = null,
            bool? selectedStatus = null);

        Task<byte[]> GenerateStatisticsPdfAsync(int month, int year);
    }
}
