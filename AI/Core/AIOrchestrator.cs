using DocumentFormat.OpenXml.Spreadsheet;
using GestionDesPresences.AI.Factories;
using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Interfaces;
using GestionDesPresences.AI.Models;
using GestionDesPresences.AI.Pipeline;
using GestionDesPresences.AI.Registry;
using GestionDesPresences.AI.Workflow;
using System.CodeDom.Compiler;

namespace GestionDesPresences.AI.Core
{
    // Notice that Gemini is not here yet. We first want the command architecture working.
    public class AIOrchestrator
    {
        //private readonly IEnumerable<IAICommand> _commands;
        //private readonly IAICommandRegistry _commands;
        ////private readonly IEnumerable<IAICommand> _commands;
        //private readonly IReadOnlyList<IAICommand> _commands;
        //private readonly ILogger<AIOrchestrator> _logger;

        //public AIOrchestrator(IEnumerable<IAICommand> commands,ILogger<AIOrchestrator> logger)
        //{
        //    _commands = commands.ToList();
        //    _logger = logger;
        //}
        private readonly IAICommandRegistry _registry;
        private readonly IIntentParser _parser;
        private readonly WorkflowPlanner _planner;
        private readonly AIWorkflowExecutor _executor;

        public AIOrchestrator(
            //IEnumerable<IAICommand> commands,
            IAICommandRegistry registry,
            IIntentParser parser,
            WorkflowPlanner planner,
            AIWorkflowExecutor executor)
        {
            _registry = registry;
            _parser = parser;
            _planner = planner;
            _executor = executor;
        }
        public async Task<AIResponse> AskAsync(string prompt)
        {
            var intent = await _parser.ParseAsync(prompt);

            // ---------- FALLBACK ----------
            if (intent.Intent == IntentType.Unknown)
            {
                // Try to interpret it as an employee lookup.
                if (!string.IsNullOrWhiteSpace(prompt))
                {
                    intent.Intent = IntentType.Employee;
                    intent.EmployeeName = prompt
                        .Replace("show", "", StringComparison.OrdinalIgnoreCase)
                        .Replace("employee", "", StringComparison.OrdinalIgnoreCase)
                        .Replace("details", "", StringComparison.OrdinalIgnoreCase)
                        .Trim();
                }
            }

            var context = new AIExecutionContext
            {
                Intent = intent,
                UserName = "CurrentUser",
                ConversationId = Guid.NewGuid().ToString()
            };

            var workflow = _planner.Create(intent);

            var responses =
                await _executor.ExecuteAsync(
                    workflow,
                    context);

            if (!responses.Any())
            {
                return AIResponseFactory.Error(
                    $"No command executed for intent '{intent.Intent}'.");
            }

            return responses.Last();
        }
        //public async Task<AIResponse> AskAsync(string prompt)
        //{
        //    var intent = _parser.Parse(prompt);
        //    switch (intent.Intent)
        //    {
        //        case IntentType.Report:

        //            return await _commands
        //                .First(c => c.Name == "Reports")
        //                .ExecuteAsync(prompt);

        //        case IntentType.Attendance:

        //            return await _commands
        //                .First(c => c.Name == "Attendance")
        //                .ExecuteAsync(prompt);

        //        default:

        //            return new AIResponse
        //            {
        //                Success = false,
        //                Message = "Sorry, I didn't understand your request."
        //            };
        //    }
        //foreach (var command in _commands)
        //{
        //    if (command.CanExecute(prompt))
        //    {
        //        return await command.ExecuteAsync(prompt);
        //    }
        //}

        ////return new AIResponse
        ////{
        ////    Success = false,
        ////    Message = "I don't know how to handle this request yet."
        ////};
    }
    }

