using GestionDesPresences.AI.Intent;
using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Pipeline
{
    public class LoggingMiddleware : IAICommandMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(
            ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task<AIResponse> InvokeAsync(
            AIExecutionContext context,
            Func<Task<AIResponse>> next)
        {
            _logger.LogInformation(
                "===== AI Command Started =====");

            _logger.LogInformation(
                "Intent : {Intent}",
                context.Intent.Intent);

            _logger.LogInformation(
                "Prompt : {Prompt}",
                context.Intent.OriginalPrompt);

            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                var response = await next();

                watch.Stop();

                _logger.LogInformation(
                    "Completed in {Time} ms",
                    watch.ElapsedMilliseconds);

                _logger.LogInformation(
                    "Success : {Success}",
                    response.Success);

                return response;
            }
            catch (Exception ex)
            {
                watch.Stop();

                _logger.LogError(
                    ex,
                    "AI command failed after {Time} ms",
                    watch.ElapsedMilliseconds);

                throw;
            }
        }
    }
}
