using Calabonga.Commandex.Engine.Processors.Base;

namespace Calabonga.Commandex.Engine.Processors.Results;

/// <summary>
/// Text file processor for commandex result
/// </summary>
public sealed class TextFileResult : ProcessorResult
{
    public TextFileResult(string fileName, string text)
    {
        FileName = fileName.EndsWith(".txt") ? fileName : $"{fileName}.txt";
        Text = text;
    }

    /// <summary>
    /// Default filename for saving file. User can change it on saving.
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// String data to save into file. There are any formats are supports.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Accepts as visitor <see cref="processor"/>
    /// </summary>
    /// <param name="processor"></param>
    public override void Accept(IProcessor processor)
        => processor.Visit(this);
}
