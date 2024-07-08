using LogWrapper.Enums;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace LogWrapper.Utilities;

public static class LoggingExtensions
{
    public static LogEventLevel ToSerilogLevel(this LogLevels logLevel) => logLevel switch
    {
        LogLevels.Verbose => LogEventLevel.Verbose,
        LogLevels.Debug => LogEventLevel.Debug,
        LogLevels.Information => LogEventLevel.Information,
        LogLevels.Warning => LogEventLevel.Warning,
        LogLevels.Error => LogEventLevel.Error,
        LogLevels.Fatal => LogEventLevel.Fatal,
        _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
    };

    public static LogLevel ToMicrosoftLogLevel(this LogLevels logLevels) => logLevels switch
    {
        LogLevels.Trace => LogLevel.Trace,
        LogLevels.Debug => LogLevel.Debug,
        LogLevels.Information => LogLevel.Information,
        LogLevels.Warning => LogLevel.Warning,
        LogLevels.Error => LogLevel.Error,
        LogLevels.Critical => LogLevel.Critical,
        _ => throw new ArgumentOutOfRangeException(nameof(logLevels), logLevels, null)
    };
}