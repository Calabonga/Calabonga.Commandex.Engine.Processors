using Calabonga.Commandex.Engine.Processors.Base;

namespace Calabonga.Commandex.Engine.Processors.Results;

/// <summary>
/// Clipboard Result command processor. Cope data to windows clipboard.
/// </summary>
public class ClipboardResult : ProcessorResult
{
    /// <summary>
    /// String data to store into clipboard
    /// </summary>
    public string ClipboardData { get; }

    public ClipboardResult(string clipboardData)
        => ClipboardData = clipboardData;

    /// <summary>
    /// Accepts as visitor <see cref="processor"/>
    /// </summary>
    /// <param name="processor"></param>
    public override void Accept(IProcessor processor)
        => processor.Visit(this);
}
