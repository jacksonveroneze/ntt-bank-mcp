namespace NttBank.Mcp.Mcp.Extensions;

internal static partial class FilterLogMessages
{
    [LoggerMessage(
        EventId = 3002,
        Level = LogLevel.Error,
        Message = "Unhandled exception in tool '{Tool}'")]
    internal static partial void LogToolUnhandledException(
        this ILogger logger, Exception ex, string tool);
}
