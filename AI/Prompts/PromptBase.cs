namespace GestionDesPresences.AI.Prompts
{
    public abstract class PromptBase
    {
        public abstract string SystemPrompt { get; }

        public abstract string BuildUserPrompt(string input);
    }
}
