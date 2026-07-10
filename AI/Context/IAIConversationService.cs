using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Context
{
    public interface IAIConversationService
    {
        AIConversationContext Get();

        void Update(AIConversationContext context);

        void Clear();

        void RememberReport(
        DownloadResult report,
        int month,
        int year);

        void RememberEmployee(
            int id,
            string name);

        void RememberIntent(IntentType intent);
    }
}