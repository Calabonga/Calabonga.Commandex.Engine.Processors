using Calabonga.Commandex.Engine.Base;
using Calabonga.Commandex.Engine.Dialogs;
using Calabonga.Commandex.Engine.Extensions;
using Calabonga.Commandex.Engine.Processors.Base;
using Microsoft.Extensions.Logging;
using System.Text;
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
            _logger.LogInformation("Commandex command type {CommandType} processing skipped. Using a DefaultResultProcessor.", command.TypeName);

            var pushState = command.IsPushToShellEnabled ? "Enabled" : "Disabled";
            var stringBuilder = new StringBuilder($"{command.DisplayName} v.{command.Version}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(command.Description);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"{nameof(command.IsPushToShellEnabled)} is {pushState}");
            stringBuilder.AppendLine();

            if (command.IsPushToShellEnabled)
            {
                var result = command.GetResult();
                if (result is null)
                {
                    stringBuilder.Append("Result is null.");
                }
                else
                {
                    try
                    {
                        var data = JsonSerializer.Serialize(result, JsonSerializerOptionsExt.Cyrillic);
                        stringBuilder.Append(data);

                    }
                    catch (Exception exception)
                    {
                        _dialogService.ShowError(exception.Message);
                        throw;
                    }
                }
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Command execution successfully completed.");

            _dialogService.ShowNotification(stringBuilder.ToString());
            return;
        }

        processorResult.Accept(_processor);
        _logger.LogInformation("Commandex command type {CommandType} processing completed.", command.TypeName);
    }
}
