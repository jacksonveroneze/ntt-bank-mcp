using System.Text.Json;
using System.Text.Json.Serialization;
using ModelContextProtocol.Protocol;

namespace NttBank.Mcp.Mcp.Mcp.Util;

public static class McpToolResult
{
    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

    public static CallToolResult Success<T>(
        string message,
        T structuredContent)
    {
        return new CallToolResult
        {
            IsError = false,
            Content =
            [
                new TextContentBlock
                {
                    Text = message,
                },
            ],
            StructuredContent = SerializeData(structuredContent),
        };
    }

    public static CallToolResult Error(
        string code,
        string message,
        IDictionary<string, string[]>? details = null)
    {
        var structuredContent = new
        {
            error = new
            {
                code,
                message,
                details,
            },
        };

        return new CallToolResult
        {
            IsError = true,
            Content =
            [
                new TextContentBlock
                {
                    Text = message,
                },
            ],
            StructuredContent = SerializeData(structuredContent),
        };
    }

    private static JsonElement SerializeData(
        object? structuredContent)
    {
        return JsonSerializer
            .SerializeToElement(structuredContent, JsonOptions);
    }
}
