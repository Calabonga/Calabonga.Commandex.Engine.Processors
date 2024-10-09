using Calabonga.Commandex.Engine.Processors.Results;

namespace Calabonga.Commandex.Engine.Processors.Base;

/// <summary>
/// Processor interface as the Visitor for the other visitor clients
/// </summary>
public interface IProcessor
{
    /// <summary>
    /// Process <see cref="TextFileResult"/>
    /// </summary>
    /// <param name="result"></param>
    void Visit(TextFileResult result);

    /// <summary>
    /// Process <see cref="ClipboardResult"/>
    /// </summary>
    /// <param name="result"></param>
    void Visit(ClipboardResult result);
}
