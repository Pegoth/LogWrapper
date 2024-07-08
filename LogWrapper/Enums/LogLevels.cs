using System.Text.Json.Serialization;

namespace LogWrapper.Enums;

/// <summary>
///     Log leves, from the most verbose to the most critical.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LogLevels
{
    /// <summary>
    ///     The most verbose log level. Same as <see cref="Trace" />.
    /// </summary>
    Verbose = 0,

    /// <summary>
    ///     The most verbose log level. Same as <see cref="Verbose" />.
    /// </summary>
    Trace = 0,

    /// <summary>
    ///     Used for debugging purposes, quite spammy, but not as much as <see cref="Trace" />.
    /// </summary>
    Debug = 1,

    /// <summary>
    ///     Used for informational messages, such as startup messages.
    /// </summary>
    Information = 2,

    /// <summary>
    ///     Used for warning messages, such as a configuration that is not optimal.
    /// </summary>
    Warning = 3,

    /// <summary>
    ///     Used for error messages, such as expected exceptions, that can be handled.
    /// </summary>
    Error = 4,

    /// <summary>
    ///     Used for fatal messages, such as exceptions that cause application shutdown. Same as <see cref="Critical" />
    /// </summary>
    Fatal = 5,

    /// <summary>
    ///     Used for fatal messages, such as exceptions that cause application shutdown. Same as <see cref="Fatal" />.
    /// </summary>
    Critical = 5
}