using System.Text.Json.Serialization;

namespace LogWrapper.Enums;

/// <summary>
///     Possible log sinks.
/// </summary>
[Flags]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LogSinks
{
    /// <summary>
    ///     Console / Debugger
    /// </summary>
    Console = 1 << 0,

    /// <summary>
    ///     File
    /// </summary>
    File = 1 << 1
}