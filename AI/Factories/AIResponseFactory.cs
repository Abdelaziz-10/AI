using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Factories
{
    public static class AIResponseFactory
    {
        public static AIResponse Success(
            string message)
        {
            return new AIResponse
            {
                Success = true,
                Message = message
            };
        }

        public static AIResponse Error(
            string message)
        {
            return new AIResponse
            {
                Success = false,
                Message = message
            };
        }

        public static AIResponse Download(
            string message,
            DownloadResult download)
        {
            return new AIResponse
            {
                Success = true,

                Action = AIAction.Download,

                Message = message,

                Data = new DownloadResult
                {
                    Url = download.Url,
                    FileName = download.FileName,
                    Size = download.Size
                }
            };
        }

        public static AIResponse Employee(
            EmployeeCardResult employee)
        {
            return new AIResponse
            {
                Success = true,

                Action = AIAction.ShowEmployee,

                Message = $"Found {employee.Name}.",

                Data = employee
            };
        }

        public static AIResponse Statistics(
            StatisticsResult statistics)
        {
            return new AIResponse
            {
                Success = true,

                Action = AIAction.ShowStatistics,

                Message = "Statistics generated.",

                Data = statistics
            };
        }

    }
}
