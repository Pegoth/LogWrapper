using System.Diagnostics;
using LogWrapper.Configurations;
using LogWrapper.Enums;
using LogWrapper.Interfaces;
using Serilog;
using Serilog.Formatting.Json;

namespace LogWrapper.Sink.File.Configurators;

public class FileSerilogConfigurator : ISerilogConfigurator
{
    public LogSinks Sink => LogSinks.File;

    public void Configure(LogConfiguration configuration, LoggerConfiguration serilogConfiguration)
    {
        using var process = Process.GetCurrentProcess();
        serilogConfiguration.WriteTo
                            .File(
                                 new JsonFormatter(renderMessage: true),
                                 Path.Combine(configuration.LogFilesPath ?? Path.Combine(Directory.GetCurrentDirectory(), "logs"), $"{process.ProcessName}_.txt"),
                                 rollingInterval: RollingInterval.Day
                             );
    }
}