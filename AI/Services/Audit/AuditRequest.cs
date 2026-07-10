using GestionDesPresences.Models;

namespace GestionDesPresences.AI.Services.Audit
{
    public class AuditRequest
    {
        public AuditAction Action { get; set; }

        public string Target { get; set; } = "";

        public string EntityName { get; set; } = "";

        public string Details { get; set; } = "";

        public string? IpAddress { get; set; }

        public string? UserAgent { get; set; }
    }
}
