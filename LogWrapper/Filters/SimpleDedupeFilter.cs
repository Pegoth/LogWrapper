using Serilog.Core;
using Serilog.Events;

namespace LogWrapper.Filters;

public class SimpleDedupeFilter : ILogEventFilter
{
    private string? _last;

    public bool IsEnabled(LogEvent logEvent)
    {
        var rendered = logEvent.RenderMessage();

        // Suppress duplicate messages
        if (rendered == _last)
            return false;

        // Allow unique messages
        _last = rendered;
        return true;
    }
}