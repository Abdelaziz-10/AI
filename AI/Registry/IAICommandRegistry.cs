using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Interfaces;

namespace GestionDesPresences.AI.Registry
{
    public interface IAICommandRegistry
    {
        IAICommand? Get(IntentType intent);

        IReadOnlyDictionary<IntentType, IAICommand> GetAll();
    }
}
