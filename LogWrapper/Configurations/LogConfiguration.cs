using System.ComponentModel;
using LogWrapper.Enums;

namespace LogWrapper.Configurations;

/// <summary>
///     Log settings
/// </summary>
public class LogConfiguration
{
    /// <summary>
    ///     Minimum log level (default: <see cref="LogLevels.Information" />).
    /// </summary>
    [DefaultValue(LogLevels.Information)]
    public LogLevels? LogLevel { get; set; } = LogLevels.Information;

    /// <summary>
    ///     Log sinks. (default: <see cref="LogSinks.Console" />).
    /// </summary>
    [DefaultValue(LogSinks.Console)]
    public LogSinks? Sinks { get; set; } = LogSinks.Console;

    /// <summary>
    ///     Directory that will hold the log files. Only used if <see cref="Sinks" /> has <see cref="LogSinks.File" />.
    /// </summary>
    public string? LogFilesPath { get; set; }
}