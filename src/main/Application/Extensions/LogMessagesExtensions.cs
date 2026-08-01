using Microsoft.Extensions.Logging;

namespace NttBankMcp.Application.Extensions;

public static partial class LogMessagesExtensions
{
    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Warning,
        Message = "{className} - {methodName} - " +
                  "Identifier: '{identifier}' - NotFound")]
    public static partial void LogNotFound(this ILogger logger,
        string className, string methodName, int identifier);
}
