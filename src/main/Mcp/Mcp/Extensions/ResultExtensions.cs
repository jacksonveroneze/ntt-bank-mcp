using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Result;
using ModelContextProtocol.Protocol;
using NttBank.Mcp.Mcp.Mcp.Util;
using Result = JacksonVeroneze.NET.Result.Result;

namespace NttBank.Mcp.Mcp.Mcp.Extensions;

[ExcludeFromCodeCoverage]
public static class ResultExtensions
{
    extension<T>(Result<T> result)
    {
        public CallToolResult ToCallToolResult()
        {
            return result.IsSuccess
                ? McpToolResult.Success(result.Value)
                : MapFailure(result);
        }
    }

    private static CallToolResult MapFailure(Result result)
    {
        return result.Type switch
        {
            ResultType.NotFound =>
                McpToolResult.NotFound(result.FirstError ?? Error.None),

            ResultType.Invalid =>
                McpToolResult.Failure("invalid", "VALIDATION_ERROR",
                    result.FirstError?.Message ?? "Requisição inválida.",
                    result.ToDictionaryByTarget),

            ResultType.Conflict =>
                McpToolResult.Failure("conflict",
                    result.FirstError?.Code ?? "CONFLICT",
                    result.FirstError?.Message ?? "Conflito de estado do recurso."),

            ResultType.RuleViolation =>
                McpToolResult.Failure("rule_violation",
                    result.FirstError?.Code ?? "RULE_VIOLATION",
                    result.FirstError?.Message ?? "Regra de negócio violada."),

            _ =>
                McpToolResult.Failure("error", "INTERNAL_ERROR",
                    "Ocorreu um erro interno ao processar a solicitação."),
        };
    }
}
