namespace GestionDesPresences.AI.Context
{
    public interface IAIUserContextService
    {
        Task<AIUserContext> GetCurrentAsync();
    }
}
