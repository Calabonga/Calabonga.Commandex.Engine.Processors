using Calabonga.Commandex.Engine.Processors.Base;

namespace Calabonga.Commandex.Engine.Processors.Extensions;

/// <summary>
/// Processor result base class
/// </summary>
public abstract class ProcessorResult : IProcessorResult
{
    /// <summary>
    /// Accepts as visitor <see cref="processor"/>
    /// </summary>
    /// <param name="processor"></param>
    public abstract void Accept(IProcessor processor);
}
