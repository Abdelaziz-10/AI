using GestionDesPresences.AI.Intent;

namespace GestionDesPresences.AI.Interfaces
{
    public interface IIntentParser
    {
        Task<AIIntent> ParseAsync(string prompt);
    }
}
