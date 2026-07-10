using GestionDesPresences.AI.Context;
using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;
public class AIConversationService : IAIConversationService
{
    private AIConversationContext _context = new();


    public AIConversationContext Get()
    {
        return _context;
    }

    public void Update(AIConversationContext context)
    {
        _context = context;
    }

    public void Clear()
    {
        _context = new();
    }

    public void RememberReport(
        DownloadResult report,
        int month,
        int year)
    {
        _context.LastReport = report;
        _context.LastMonth = month;
        _context.LastYear = year;
        _context.LastIntent = IntentType.Report;
        _context.LastUpdated = DateTime.UtcNow;
    }

    public void RememberEmployee(
        int id,
        string name)
    {
        _context.LastEmployeeId = id;
        _context.LastEmployeeName = name;
        _context.LastUpdated = DateTime.UtcNow;
    }

    public void RememberIntent(IntentType intent)
    {
        _context.LastIntent = intent;
        _context.LastUpdated = DateTime.UtcNow;
    }
}