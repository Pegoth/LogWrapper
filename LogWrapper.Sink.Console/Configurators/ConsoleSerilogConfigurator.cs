using LogWrapper.Configurations;
using LogWrapper.Enums;
using LogWrapper.Interfaces;
using Serilog;

namespace LogWrapper.Sink.Console.Configurators;

public class ConsoleSerilogConfigurator : ISerilogConfigurator
{
    public LogSinks Sink => LogSinks.Console;

    public void Configure(LogConfiguration configuration, LoggerConfiguration serilogConfiguration)
    {
        serilogConfiguration.WriteTo
                            .Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}]: {Message:lj}{NewLine}{Exception}");
    }
}