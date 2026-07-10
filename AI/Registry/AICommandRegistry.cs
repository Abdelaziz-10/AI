using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Interfaces;

namespace GestionDesPresences.AI.Registry
{
    public class AICommandRegistry : IAICommandRegistry
    {
        private readonly Dictionary<IntentType, IAICommand> _commands;
        private readonly ILogger<AICommandRegistry> _logger;

        public AICommandRegistry(
            IEnumerable<IAICommand> commands,
            ILogger<AICommandRegistry> logger)
        {
                    _logger = logger;

                    _commands = new Dictionary<IntentType, IAICommand>();

            foreach (var command in commands)
            {
                if (_commands.ContainsKey(command.Intent))
                {
                    _logger.LogInformation(
                        "Registered AI command {Intent}",
                            command.Intent);
                }

                _commands.Add(command.Intent, command);
            }
        }

        public IAICommand? Get(IntentType intent)
        {
            _commands.TryGetValue(intent, out var command);

            return command;
        }

        public IReadOnlyDictionary<IntentType, IAICommand> GetAll()
        {
            return _commands;
        }

        public bool Exists(IntentType intent)
        {
            return _commands.ContainsKey(intent);
        }
    }
}
