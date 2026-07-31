using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using ModelContextProtocol.Protocol;
using NttBank.Mcp.Mcp.Mcp.Util;

namespace NttBank.Mcp.Mcp.Mcp.Extensions;

[ExcludeFromCodeCoverage]
public static class ValidationResultExtensions
{
    extension(ValidationResult validationResult)
    {
        public CallToolResult ToCallToolResultError(string? message = null)
        {
            ArgumentNullException.ThrowIfNull(validationResult);
        
            var erros = validationResult.ToDictionary();

            return McpToolResult.Error(
                code: "VALIDATION_ERROR",
                message: message ?? "Invalid input.",
                details: erros);
        }

        internal IDictionary<string, string[]> ToValidationProblemDictionary()
        {
            return validationResult.Errors
                .GroupBy(
                    error => error.PropertyName,
                    comparer: StringComparer.OrdinalIgnoreCase)
                .ToDictionary(
                    group => group.Key,
                    group => group
                        .Select(error => error.ErrorMessage)
                        .ToArray(),
                    StringComparer.OrdinalIgnoreCase);
        }

        internal IResult ToValidationProblem()
        {
            return Results.ValidationProblem(
                validationResult.ToValidationProblemDictionary());
        }
    }
}
