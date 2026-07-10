using GestionDesPresences.AI.Context;
using GestionDesPresences.AI.Factories;
using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Pipeline;

namespace GestionDesPresences.AI.Commands
{
    public class DownloadCommand : AICommandBase
    {
        private readonly IAIConversationService _conversation;

        public DownloadCommand(IAIConversationService conversation)
        {
            _conversation = conversation;
        }

        public override IntentType Intent => IntentType.Download;

        public override Task<AIResponse> ExecuteAsync(AIExecutionContext context)
        {
            if (context.Conversation?.LastReport == null)
            {
                return Task.FromResult(
                    AIResponseFactory.Error(
                        "There isn't a report to download yet."));
            }

            return Task.FromResult(
                AIResponseFactory.Download(
                    "Here's your latest report.",
                    context.Conversation.LastReport));
        }
    }
}