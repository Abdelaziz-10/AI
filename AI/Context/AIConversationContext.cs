using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Context
{
    public class AIConversationContext
    {
            // Last generated file
            public DownloadResult? LastReport { get; set; }

            // Employee context
            public int? LastEmployeeId { get; set; }

            public string? LastEmployeeName { get; set; }

            // Date context
            public int? LastMonth { get; set; }

            public int? LastYear { get; set; }

            // Output format
            public string? LastFormat { get; set; }

            // Last detected intent
            public IntentType? LastIntent { get; set; }

            // Dashboard/Navigation
            public string? LastDashboard { get; set; }

            // Statistics
            public object? LastStatistics { get; set; }

            // Timestamp
            public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        }
    }
