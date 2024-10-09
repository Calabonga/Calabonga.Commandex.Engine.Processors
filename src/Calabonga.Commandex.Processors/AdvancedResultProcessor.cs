using Calabonga.Commandex.Engine.Base;
using Calabonga.Commandex.Engine.Processors.Base;
using Microsoft.Extensions.Logging;

namespace Calabonga.Commandex.Engine.Processors;

/// <summary>
/// Implementation for <see cref="IResultProcessor"/> with some advanced features for result processing
/// </summary>
public class AdvancedResultProcessor : IResultProcessor
{
    private readonly IProcessor _processor;
    private readonly ILogger<AdvancedResultProcessor> _logger;

    public AdvancedResultProcessor(IProcessor processor, ILogger<AdvancedResultProcessor> logger)
    {
        _processor = processor;
        _logger = logger;
    }

    public void ProcessCommand(ICommandexCommand command)
    {
        if (command.GetResult() is not IProcessorResult result)
        {
            _logger.LogInformation("Commandex command type {CommandType} processing skipped.", command.TypeName);
            return;
        }

        result.Accept(_processor);
        _logger.LogInformation("Commandex command type {CommandType} processing completed.", command.TypeName);
    }
}
