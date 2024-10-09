using Calabonga.Commandex.Engine.Processors.Base;

namespace Calabonga.Commandex.Engine.Processors.Results;

/// <summary>
/// Text file processor for commandex result
/// </summary>
public sealed class TextFileResult : ProcessorResult, IProcessorResult
{
    public TextFileResult(string fileName, string text)
    {
        FileName = fileName.EndsWith(".txt") ? fileName : $"{fileName}.txt";
        Text = text;
    }

    public string FileName { get; }

    public string Text { get; }

    public override void Accept(IProcessor processor)
        => processor.Visit(this);
}
