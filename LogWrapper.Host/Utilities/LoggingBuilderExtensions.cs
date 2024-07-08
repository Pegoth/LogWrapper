using LogWrapper.Configurations;
using LogWrapper.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LogWrapper.Host.Utilities;

public static class LoggingBuilderExtensions
{
    /// <summary>
    ///     Switches Microsoft logging to Serilog via LogWrapper.
    /// </summary>
    /// <param name="builder">The log configurator.</param>
    /// <param name="configuration">The configuration to read. In from HostBuilderContext in .NET/.NET Core.</param>
    /// <returns>The log configurator (same as the input) to be able to chain commands.</returns>
    public static ILoggingBuilder UseLogWrapper(this ILoggingBuilder builder, IConfiguration configuration)
    {
        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration), "Configuration is required.");

        // Register the configuration to the DI
        builder.Services.Configure<LogConfiguration>(configuration.GetSection("LogSettings"));

        // Setup logger
        builder.Services.AddSingleton(LoggerUtils.Configure(configuration));

        // Setup Microsoft logger
        builder.ClearProviders();
        builder.SetMinimumLevel(LogLevel.Trace);
        builder.AddSerilog(dispose: true);

        return builder;
    }
}