namespace Calabonga.Commandex.Engine.Processors.Base;

/// <summary>
/// Processor result marker
/// </summary>
public interface IProcessorResult
{
    /// <summary>
    /// Accepts as visitor <see cref="IProcessor"/>
    /// </summary>
    /// <param name="processor"></param>
    void Accept(IProcessor processor);
}
