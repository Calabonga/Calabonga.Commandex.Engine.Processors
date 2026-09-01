using Calabonga.Commandex.Engine.Processors.Results;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace Calabonga.Commandex.Engine.Processors.Base;

/// <summary>
/// Processor object used as the Visitor for the other visitor clients
/// </summary>
public sealed class Processor : IProcessor
{
    private readonly ILogger<Processor> _logger;

    public Processor(ILogger<Processor> logger) => _logger = logger;

    /// <summary>
    /// Process <see cref="TextFileResult"/>
    /// </summary>
    /// <param name="result"></param>
    public void Visit(TextFileResult result)
    {
        var ext = Path.GetExtension(result.FileName);
        var saveFileDialog = new SaveFileDialog
        {
            FileName = result.FileName,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Filter = $"Commandex file *{ext}|*{ext}"
        };

        if (saveFileDialog.ShowDialog() != true)
        {
            _logger.LogInformation("[PROCESSOR] TextFileResult: save canceled by user ({FileName})", result.FileName);
            return;
        }

        File.WriteAllText(saveFileDialog.FileName, result.Text);
        _logger.LogInformation("[PROCESSOR] TextFileResult: {Length} chars saved to {Path}", result.Text.Length, saveFileDialog.FileName);
    }

    /// <summary>
    /// Process <see cref="ClipboardResult"/>
    /// </summary>
    /// <param name="result"></param>
    public void Visit(ClipboardResult result)
    {
        Clipboard.SetText(result.ClipboardData);
        _logger.LogInformation("[PROCESSOR] ClipboardResult: {Length} chars copied to clipboard", result.ClipboardData.Length);
    }
}
