using GestionDesPresences.AI.Context;
using GestionDesPresences.Data;
using GestionDesPresences.Hubs;
using GestionDesPresences.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GestionDesPresences.AI.Services.Audit
{
    public class AuditAIService : IAuditAIService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAIUserContextService _userContext;

        private readonly IHubContext<AuditHub> _hub;

    public AuditAIService(
        ApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor,
        IHubContext<AuditHub> hub,
        IAIUserContextService userContext)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _hub = hub;
        _userContext = userContext;
    }

    private void DetectSuspiciousActivity(AuditLog log)
    {
        // Example rule 1: Critical actions
        if (log.Action == AuditAction.DeviceRevoked)
        {
            log.RiskScore += 50;
            log.IsSuspicious = true;
        }

        // Example rule 2: Multiple actions in short time
        var recentCount = _context.AuditLogs
            .Where(a => a.Actor == log.Actor &&
                        a.Timestamp > DateTime.UtcNow.AddMinutes(-2))
            .Count();

        if (recentCount > 5)
        {
            log.RiskScore += 30;
            log.IsSuspicious = true;
        }

        // Example rule 3: High severity
        if (log.Severity == AuditSeverity.Critical)
        {
            log.RiskScore += 40;
        }

        if (log.RiskScore >= 60)
        {
            log.IsSuspicious = true;
        }
    }


    public async Task LogAsync(
        string action,
        string target,
        string entityName,
        string details = null
        )
    {
            var user = await _userContext.GetCurrentAsync();

            var httpContext = _httpContextAccessor.HttpContext;

        var ip = httpContext?.Connection.RemoteIpAddress?.ToString();
        var userAgent = httpContext?.Request.Headers["User-Agent"].ToString();

        var audit = new AuditLog
        {
            Action = Enum.TryParse<AuditAction>(action, out var parsedAction)
                        ? parsedAction
                        : AuditAction.Login,

            Actor = string.IsNullOrWhiteSpace(user.UserName)
            ? "SYSTEM"
            : $"{user.UserName} (AI)",
            Target = target,
            EntityName = entityName,
            Details = details,
            Timestamp = DateTime.UtcNow,
            IpAddress = ip,
            UserAgent = userAgent,
            Severity = AuditSeverity.Info
        };

        DetectSuspiciousActivity(audit);

        _context.AuditLogs.Add(audit);
        await _context.SaveChangesAsync();


        // 🚀 REAL-TIME BROADCAST HERE
        await _hub.Clients.All.SendAsync("ReceiveAuditLog", new
        {
            timestamp = audit.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
            actor = audit.Actor,
            action = audit.Action.ToString(),
            severity = audit.Severity.ToString(),
            details = audit.Details.ToString(),
            target = audit.Target.ToString(),
            entityName = audit.EntityName.ToString(),
            ipAdress = audit.IpAddress,
            userAgent = audit.UserAgent,
            riskScore = audit.RiskScore,
            isSuspicious = audit.IsSuspicious

        });
    }

    // Compte les logs récents pour un acteur + action sur une fenêtre donnée
    public async Task<int> CountRecentAsync(string actor, string action, TimeSpan window)
    {
        if (string.IsNullOrWhiteSpace(actor) || string.IsNullOrWhiteSpace(action))
            return 0;

        // Tenter de parser l'action en enum AuditAction
        if (Enum.TryParse<AuditAction>(action, out var parsedAction))
        {
            var cutoff = DateTime.UtcNow - window;
            return await _context.AuditLogs
                .Where(a => a.Actor == actor && a.Action == parsedAction && a.Timestamp > cutoff)
                .CountAsync();
        }

        // Si parse échoue, essayer de comparer la représentation texte (défensive)
        var cutoffText = DateTime.UtcNow - window;
        return await _context.AuditLogs
            .Where(a => a.Actor == actor && a.Action.ToString() == action && a.Timestamp > cutoffText)
            .CountAsync();
    }
}


}