using System.Text.Json;
using System.Text.Json.Serialization;
using JacksonVeroneze.NET.Result;
using ModelContextProtocol.Protocol;

namespace NttBankMcp.Mcp.Util;

public static class McpToolResult
{
    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

    public static CallToolResult Success<T>(
        T data,
        string? message = null)
    {
        return Build(
            isError: false,
            text: message ?? "Operação concluída com sucesso.",
            payload: new
            {
                status = "success",
                data,
            });
    }

    public static CallToolResult NotFound(
        Error error)
    {
        return Build(
            isError: false,
            text: error.Message,
            payload: new
            {
                status = "not_found",
                found = false,
                data = (object?)null,
                reason = new
                {
                    code = error.Code,
                    message = error.Message,
                },
            });
    }

    public static CallToolResult Failure(
        string status,
        string code,
        string message,
        IDictionary<string, IEnumerable<string>>? details = null)
    {
        return Build(
            isError: true,
            text: message,
            payload: new
            {
                status, error = new
                {
                    code,
                    message,
                    details,
                },
            });
    }

    private static CallToolResult Build(
        bool isError,
        string text,
        object payload)
    {
        return new CallToolResult
        {
            IsError = isError,
            Content =
            [
                new TextContentBlock
                {
                    Text = text,
                },
            ],
            StructuredContent = JsonSerializer
                .SerializeToElement(payload, JsonOptions),
        };
    }
}
