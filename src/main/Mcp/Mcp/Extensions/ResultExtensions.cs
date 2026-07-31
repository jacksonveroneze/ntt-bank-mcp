using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Result;
using ModelContextProtocol.Protocol;
using NttBank.Mcp.Mcp.Mcp.Util;

namespace NttBank.Mcp.Mcp.Mcp.Extensions;

[ExcludeFromCodeCoverage]
public static class ResultExtensions
{
    extension<T>(Result<T> result)
    {
        public CallToolResult ToCallToolResultError()
        {
            var res = McpToolResult.Error(
                code: "APPLICATION_ERROR",
                message: result.FirstError?.Message ?? string.Empty);

            return res;
        }

        public CallToolResult ToCallToolResultSuccess()
        {
            var res = McpToolResult.Success(
                message: "Successfully",
                structuredContent: new
                {
                    data = result.Value,
                });
        
            return res;
        }
    }
    
    extension(JacksonVeroneze.NET.Result.Result result)
    {
        public CallToolResult ToCallToolResultError()
        {
            var res = McpToolResult.Error(
                code: "APPLICATION_ERROR",
                message: result.FirstError?.Message ?? string.Empty);

            return res;
        }

        public CallToolResult ToCallToolResultSuccess()
        {
            var res = McpToolResult.Success(
                message: "Successfully",
                structuredContent: new
                {
                    data = "Successfully",
                });
        
            return res;
        }
    }
}
