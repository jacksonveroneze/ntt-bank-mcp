using System.ComponentModel;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NttBankMcp.Application.Customers.GetCustomer;
using NttBankMcp.Mcp.Extensions;
using NttBankMcp.Mcp.Security;

namespace NttBankMcp.Mcp.Tools.Customer;

[McpServerToolType]
public sealed class GetCustumerByIdTool(
    IValidator<GetCustomerRequest> validator,
    IGetCustomerUseCase useCase)
{
    #region constants

    private const string GetCustomerToolName = "get_customer_by_id";
    private const string GetCustomerToolTitle = "Get Customer by ID";

    private const string GetCustomerToolDesc =
        """
        Retrieves full profile data for a single customer by their unique numeric ID.
        Returns customer details on success, or a not_found result if no customer
        exists with the given ID. Use this tool when you need to look up a specific customer.
        """;

    private const string GetCustomerIdParamDesc =
        "The unique numeric identifier of the customer (must be greater than zero).";

    #endregion

    [McpServerTool(
        Name = GetCustomerToolName, 
        Title = GetCustomerToolTitle)]
    [Description(GetCustomerToolDesc)]
    [Authorize(Policy = AuthorizationPolicies.CustomerRead)]
    public async Task<CallToolResult> GetByIdAsync(
        [Description(GetCustomerIdParamDesc)] int customerId,
        CancellationToken cancellationToken)
    {
        var request = new GetCustomerRequest(customerId);

        var validation = await validator
            .ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
        {
            return validation.ToCallToolResultError();
        }

        var result = await useCase
            .ExecuteAsync(request, cancellationToken);

        return result.ToCallToolResult();
    }
}
