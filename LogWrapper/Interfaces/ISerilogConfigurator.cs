using LogWrapper.Configurations;
using LogWrapper.Enums;
using Serilog;

namespace LogWrapper.Interfaces;

public interface ISerilogConfigurator
{
    /// <summary>
    ///     Sink that this configurator can handle.
    /// </summary>
    LogSinks Sink { get; }

    /// <summary>
    ///     Configures the given <paramref name="serilogConfiguration" /> based on the settings in <paramref name="configuration" />.
    /// </summary>
    /// <param name="configuration">The NewLine.Logging style configuration.</param>
    /// <param name="serilogConfiguration">The Serilog configuration that will be configured by this method.</param>
    void Configure(LogConfiguration configuration, LoggerConfiguration serilogConfiguration);
}