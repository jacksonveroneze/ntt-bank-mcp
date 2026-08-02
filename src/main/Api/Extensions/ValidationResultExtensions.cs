using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using ModelContextProtocol.Protocol;
using NttBankMcp.Api.Util;

namespace NttBankMcp.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class ValidationResultExtensions
{
    public static CallToolResult ToCallToolResultError(
        this ValidationResult validationResult)
    {
        ArgumentNullException.ThrowIfNull(validationResult);
        
        var details = validationResult.Errors
            .GroupBy(
                e => e.PropertyName,
                StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).AsEnumerable(),
                StringComparer.OrdinalIgnoreCase);

        return McpToolResult.Failure(
            status:  "invalid",
            code:    "VALIDATION_ERROR",
            message: "One or more input fields are invalid.",
            details: details);
    }
}
