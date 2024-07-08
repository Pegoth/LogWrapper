using LogWrapper.Configurations;
using LogWrapper.Enums;
using LogWrapper.Interfaces;
using Serilog;

namespace LogWrapper.Console.Configurators;

internal class DebugSerilogConfigurator : ISerilogConfigurator
{
    public LogSinks Sink => LogSinks.Console;

    public void Configure(LogConfiguration configuration, LoggerConfiguration serilogConfiguration)
    {
        serilogConfiguration.WriteTo
                            .Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}]: {Message:lj}{NewLine}{Exception}");
    }
}