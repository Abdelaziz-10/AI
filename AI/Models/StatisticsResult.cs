namespace GestionDesPresences.AI.Models
{
    public class StatisticsResult
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public int TotalEmployees { get; set; }

        public int TotalPresent { get; set; }

        public int TotalAbsent { get; set; }

        public double AttendanceRate { get; set; }

        public double PreviousMonthRate { get; set; }

        public double Difference { get; set; }

        public string Insight { get; set; } = "";

        public string DashboardUrl { get; set; } = "/Dashboard";

        public string PdfUrl { get; set; } = "";

        public string ExcelUrl { get; set; } = "";
        public List<MonthlyChartPoint> MonthlyTrend { get; set; } = [];
    }
    public class MonthlyChartPoint
    {
        public string Label { get; set; } = "";

        public int Present { get; set; }

        public int Absent { get; set; }
    }
}
