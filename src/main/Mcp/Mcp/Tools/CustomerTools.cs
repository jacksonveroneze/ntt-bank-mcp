using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NttBank.Mcp.Application.Customers.GetCustomer;
using NttBank.Mcp.Mcp.Mcp.Extensions;

namespace NttBank.Mcp.Mcp.Mcp.Tools;

[McpServerToolType]
[AllowAnonymous]
public sealed class CustomerTools
{
    #region constants

    private const string GetByIdOrderToolName = "get_customer_by_id";
    private const string GetByIdOrderToolTitle = "get customer by id";
    private const string GetByIdOrderToolDesc = "Retrieves a customer by its identifier.";
    private const string GetByIdIdParamDesc = "The identifier of the customer to retrieve.";

    #endregion

    [McpServerTool(
        Name = GetByIdOrderToolName,
        Title = GetByIdOrderToolTitle
    )]
    [Description(GetByIdOrderToolDesc)]
    [McpMeta("category", "weather")]
    [McpMeta("recommendedModel", "gpt-4")]
    public async Task<CallToolResult> GetByIdAsync(
        [FromServices] IGetCustomerUseCase useCase,
        [Description(GetByIdIdParamDesc)] int id,
        CancellationToken cancellationToken)
    {
        var request = new GetCustomerRequest(id);

        var result = await useCase
            .ExecuteAsync(request, cancellationToken);

        return result.IsSuccess
            ? result.ToCallToolResultSuccess()
            : result.ToCallToolResultError();
    }
}
