using Calabonga.Commandex.Engine.Base;
using Calabonga.Commandex.Engine.Dialogs;
using Calabonga.Commandex.Engine.Extensions;
using Calabonga.Commandex.Engine.Processors.Base;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Calabonga.Commandex.Engine.Processors;

/// <summary>
/// Implementation for <see cref="IResultProcessor"/> with some advanced features for result processing
/// </summary>
public class AdvancedResultProcessor : IResultProcessor
{
    private readonly IDialogService _dialogService;
    private readonly IProcessor _processor;
    private readonly ILogger<AdvancedResultProcessor> _logger;

    public AdvancedResultProcessor(
        IDialogService dialogService,
        IProcessor processor,
        ILogger<AdvancedResultProcessor> logger)
    {
        _dialogService = dialogService;
        _processor = processor;
        _logger = logger;
    }

    public void ProcessCommand(ICommandexCommand command)
    {
        if (command.GetResult() is not IProcessorResult processorResult)
        {
            _logger.LogInformation("[RESULT PROCESSOR] Commandex command type {CommandType} processing skipped. Using a {defaultProcessor}", command.TypeName, nameof(ProcessorResult));

            var pushState = command.IsPushToShellEnabled ? "Enabled" : "Disabled";
            _logger.LogInformation("[RESULT PROCESSOR] {DisplayName} v.{Version}", command.DisplayName, command.Version);
            _logger.LogInformation("[RESULT PROCESSOR] {Description}", command.Description);
            _logger.LogInformation("[RESULT PROCESSOR] IsPushToShellEnabled: {PushState}", pushState);

            if (command.IsPushToShellEnabled)
            {
                var result = command.GetResult();
                if (result is null)
                {
                    _logger.LogInformation("[RESULT PROCESSOR] Result is NULL.");
                }
                else
                {
                    try
                    {
                        var data = JsonSerializer.Serialize(result, JsonSerializerOptionsExt.Cyrillic);
                        _logger.LogInformation("[RESULT PROCESSOR] {Data}", data);

                    }
                    catch (Exception exception)
                    {
                        _dialogService.ShowError(exception.Message);
                        throw;
                    }
                }
            }
            _logger.LogInformation("[RESULT PROCESSOR] Command execute successfully completed.");
            return;
        }

        processorResult.Accept(_processor);
        _logger.LogInformation("[RESULT PROCESSOR] Commandex command type {CommandType} processing completed.", command.TypeName);
    }
}
