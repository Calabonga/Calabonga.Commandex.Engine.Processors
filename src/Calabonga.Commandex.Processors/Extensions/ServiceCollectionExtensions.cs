using Calabonga.Commandex.Engine.Base;
using Calabonga.Commandex.Engine.Processors.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Calabonga.Commandex.Engine.Processors.Extensions;

/// <summary>
/// Extension from Dependency Injection container
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register advanced result processor <see cref="IResultProcessor"/> for <see cref="ICommandexCommand"/> result post-processing.
    /// </summary>
    /// <param name="source"></param>
    public static void AddAdvancedResultProcessor(this IServiceCollection source)
    {
        source.AddScoped<IProcessor, Processor>();
        source.TryAddScoped<IResultProcessor, AdvancedResultProcessor>();
    }
}
