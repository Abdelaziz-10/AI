using GestionDesPresences.AI.Context;
using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;
using GestionDesPresences.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace GestionDesPresences.AI.Pipeline
{
    public class AIExecutionContext
    {
        public Guid ExecutionId { get; } = Guid.NewGuid();

        public AIIntent Intent { get; set; }

        public AIResponse? Response { get; set; }

        public ClaimsPrincipal? User { get; set; }

        public string? UserName { get; set; }

        public string? ConversationId { get; set; }

        public AIConversationContext? Conversation { get; set; }

        public Stopwatch Stopwatch { get; }

        public CancellationToken CancellationToken { get; set; }

        public Dictionary<string, object> Items { get; }

        public List<string> Logs { get; }

        public List<string> Warnings { get; }
        public Collaborateur? Employee { get; set; }
        

        public DateTime StartedAt { get; } = DateTime.UtcNow;

        public string? UserId { get; set; }
        public EmployeeCardResult? EmployeeCard { get; set; }

    }
}
