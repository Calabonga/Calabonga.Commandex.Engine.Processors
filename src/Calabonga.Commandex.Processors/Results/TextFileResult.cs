using Calabonga.Commandex.Engine.Processors.Base;
using System.IO;

namespace Calabonga.Commandex.Engine.Processors.Results;

/// <summary>
/// Text file processor for commandex result
/// </summary>
public sealed class TextFileResult : ProcessorResult
{
    public TextFileResult(string fileName, string text)
    {
        FileName = Path.HasExtension(fileName) ? fileName : $"{fileName}.txt";
        Text = text;
    }

    /// <summary>
    /// Default filename for saving file. User can change it on saving.
    /// When <paramref name="fileName"/> has no extension, <c>.txt</c> is appended;
    /// an explicit extension (<c>.csv</c>, <c>.json</c>, <c>.sql</c>, …) is kept as is.
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
