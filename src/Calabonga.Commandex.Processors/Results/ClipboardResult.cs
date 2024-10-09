using Calabonga.Commandex.Engine.Processors.Base;

namespace Calabonga.Commandex.Engine.Processors.Results;

public class ClipboardResult : ProcessorResult
{
    public string ClipboardData { get; }

    public ClipboardResult(string clipboardData)
        => ClipboardData = clipboardData;

    public override void Accept(IProcessor processor)
        => processor.Visit(this);
}
