namespace GestionDesPresences.AI.Services.Audit
{
    public partial interface IAuditAIService
    {
        Task LogAsync(
            string action,
            string target,
            string entityName,
            string details = null
        );

        // Nouvelle méthode pour compter les logs récents
        Task<int> CountRecentAsync(string actor, string action, TimeSpan window);
    }
}
