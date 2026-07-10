using GestionDesPresences.Controllers;
using GestionDesPresences.Data;
using GestionDesPresences.Helper;
using GestionDesPresences.Models;
using GestionDesPresences.PDF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QuestPDF.Fluent;

namespace GestionDesPresences.AI.Services.Reports
{
    public class PresenceReportService : IPresenceReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<PresencesController> _localizer;
        public PresenceReportService(ApplicationDbContext context, IStringLocalizer<PresencesController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<byte[]> GenerateEmployeeHistoryPdfAsync(
            int employeeId,
            DateTime? selectedDate = null,
            bool? selectedStatus = null)
        {
            var collaborateur =
                await _context.Collaborateurs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == employeeId);

            if (collaborateur == null)
                return Array.Empty<byte>();

            var query = _context.Presences
                .AsNoTracking()
                .Include(p => p.Collaborateur)
                .Where(p => p.CollaborateurId == employeeId);

            if (selectedDate.HasValue)
            {
                query = query.Where(p =>
                    p.Date.Date == selectedDate.Value.Date);
            }

            if (selectedStatus.HasValue)
            {
                query = query.Where(p =>
                    p.EstPresent == selectedStatus.Value);
            }

            var data = await query
                .OrderByDescending(p => p.Date)
                .ToListAsync();

            if (!data.Any())
                return Array.Empty<byte>();

            var document = new PresenceHistoryPdfDocument(
                data,
                collaborateur.Nom,
                _localizer);

            return document.GeneratePdf();
        }
        // This method is similar to GenerateEmployeeHistoryPdfAsync but does not check for the existence of the employee in the Collaborateurs table.
        public async Task<byte[]> GenerateEmployeeHistoryPdfAsync0(int employeeId, DateTime? selectedDate = null, bool? selectedStatus = null)
        {
            var query = _context.Presences
                .AsNoTracking()
                .Include(p => p.Collaborateur)
                .Where(p => p.CollaborateurId == employeeId);

            if (selectedDate.HasValue)
            {
                query = query.Where(p => p.Date == selectedDate.Value);
            }

            if (selectedStatus.HasValue)
            {
                query = query.Where(p => p.EstPresent == selectedStatus.Value);
            }

            var data = await query
                .OrderByDescending(p => p.Date)
                .ToListAsync();

            if (!data.Any())
                return Array.Empty<byte>();

            var employeeName = data.First().Collaborateur?.Nom ?? $"ID {employeeId}";

            var document = new PresenceHistoryPdfDocument(
                data,
                employeeName,
                _localizer);

            return document.GeneratePdf();
        }



        public async Task<byte[]> GenerateMonthlyPdfAsync(int month, int year)
        {
            var data = await _context.Presences
                .AsNoTracking()
                .Include(p => p.Collaborateur)
                .Where(p => p.Date.Month == month &&
                            p.Date.Year == year)
                .Select(p => new PresenceExportModel
                {
                    Date = p.Date,
                    Collaborateur = p.Collaborateur.Nom,
                    EstPresent = p.EstPresent
                })
                .OrderBy(p => p.Date)
                .ToListAsync();

            var document =
                new PresencePdfDocument(
                    data,
                    month,
                    year,
                    _localizer);

            return document.GeneratePdf();
        }

        public Task<byte[]> GenerateStatisticsPdfAsync(int month, int year)
        {
            throw new NotImplementedException();
        }
    }
}