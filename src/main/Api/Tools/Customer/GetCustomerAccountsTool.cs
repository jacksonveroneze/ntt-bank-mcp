using System.ComponentModel;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NttBankMcp.Api.Extensions;
using NttBankMcp.Api.Security;
using NttBankMcp.Application.Customers.GetCustomerAccounts;

namespace NttBankMcp.Api.Tools.Customer;

[McpServerToolType]
public sealed class GetCustomerAccountsTool(
    IValidator<GetCustomerAccountsRequest> validator,
    IGetCustomerAccountsUseCase useCase)
{
    #region constants

    private const string GetCustomerAccountsToolName = "get_customer_accounts";
    private const string GetCustomerAccountsToolTitle = "Get Customer Accounts";

    private const string GetCustomerAccountsToolDesc =
        """
        Lists every bank account owned by a single customer, identified by their
        unique numeric ID. Each account includes its account ID, branch ID, account
        type, balance, open date and status. A customer with no accounts is a valid
        outcome: the tool succeeds and returns an empty account list. Use this tool
        when you need the accounts, balances or account status of a known customer.
        """;

    private const string GetCustomerIdParamDesc =
        "The unique numeric identifier of the customer (must be greater than zero).";

    #endregion

    [McpServerTool(
        Name = GetCustomerAccountsToolName,
        Title = GetCustomerAccountsToolTitle)]
    [Description(GetCustomerAccountsToolDesc)]
    [Authorize(Policy = AuthorizationPolicies.CustomerAccountsRead)]
    public async Task<CallToolResult> GetAccountsAsync(
        [Description(GetCustomerIdParamDesc)] int customerId,
        CancellationToken cancellationToken)
    {
        var request = new GetCustomerAccountsRequest(customerId);

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
