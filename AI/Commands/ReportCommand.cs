using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using GestionDesPresences.AI.Context;
using GestionDesPresences.AI.Factories;
using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Pipeline;
using GestionDesPresences.AI.Resolvers;
using GestionDesPresences.AI.Services.Audit;
using GestionDesPresences.AI.Services.Reports;
using GestionDesPresences.AI.Services.Storage;
using GestionDesPresences.Models;
using GestionDesPresences.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Org.BouncyCastle.Asn1.X509;

namespace GestionDesPresences.AI.Commands
{
    public class ReportCommand : AICommandBase
    {
        private readonly IPresenceReportService _reportService;
        private readonly IFileStorageService _storageService;


        private readonly IAuditAIService _auditService;


        public ReportCommand(
        IPresenceReportService reportService,
        IFileStorageService storageService,
        IAuditAIService auditService)
        {
            _reportService = reportService;
            _storageService = storageService;
            _auditService = auditService;
        }

        public override IntentType Intent => IntentType.Report;

        public override async Task<AIResponse> ExecuteAsync(
    AIExecutionContext context)
        {
            int month = context.Intent.Month ?? DateTime.Today.Month;
            int year = context.Intent.Year ?? DateTime.Today.Year;

            byte[] pdf;
            string fileName;

            if (context.Employee != null)
            {
                pdf = await _reportService.GenerateEmployeeHistoryPdfAsync(
                    context.Employee.Id);

                fileName =
                    $"Rapport_History_{Guid.NewGuid():N}.pdf";

                await _auditService.LogAsync(
                    "EXPORT_PDF",
                    context.Employee.Nom,
                    "AIReport",
                    $"AI generated employee report for {context.Employee.Nom}");
            }
            else
            {
                pdf =
                    await _reportService.GenerateMonthlyPdfAsync(month, year);

                fileName =
                    $"Rapport_Presences_{year}_{month:00}_{Guid.NewGuid():N}.pdf";

                await _auditService.LogAsync(
                    "EXPORT_PDF",
                    $"Monthly Report {month}/{year}",
                    "AIReport",
                    $"AI generated monthly report.");
            }

            if (pdf.Length == 0)
                return AIResponseFactory.Error("Unable to generate report.");

            var url =
                await _storageService.SavePdfAsync(pdf, fileName);

            return AIResponseFactory.Download(
                "Report generated successfully.",
                new DownloadResult
                {
                    Url = url,
                    FileName = fileName,
                    Size = pdf.Length
                });
        }
    }
}
