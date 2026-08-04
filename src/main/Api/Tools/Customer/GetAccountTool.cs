using System.ComponentModel;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NttBankMcp.Api.Extensions;
using NttBankMcp.Api.Security;
using NttBankMcp.Application.Customers.GetAccount;

namespace NttBankMcp.Api.Tools.Customer;

[McpServerToolType]
public sealed class GetAccountTool(
    IValidator<GetAccountRequest> validator,
    IGetAccountUseCase useCase)
{
    #region constants

    private const string GetAccountToolName = "get_account";
    private const string GetAccountToolTitle = "Get Account";

    private const string GetAccountToolDesc =
        """
        Returns the data and current state of ONE specific bank account — account
        type, current balance, open date and status — given its account_id. Use
        this when the user asks about the balance, situation or details of an
        account already identified by account_id.

        Do NOT use this for: listing a customer's accounts (use
        get_customer_accounts); querying account transactions/entries (use
        list_account_transactions); getting totals, averages or trends (use
        summarize_account_transactions).

        Requires a known account_id. If you only have the customer, resolve their
        accounts first before calling this tool.
        """;

    private const string AccountIdParamDesc =
        "The unique numeric identifier of the account (must be greater than zero).";

    #endregion

    [McpServerTool(
        Name = GetAccountToolName,
        Title = GetAccountToolTitle)]
    [Description(GetAccountToolDesc)]
    [Authorize(Policy = AuthorizationPolicies.CustomerAccountsRead)]
    public async Task<CallToolResult> GetAccountAsync(
        [Description(CustomerToolConstants.CustomerIdParamDesc)] int customerId,
        [Description(AccountIdParamDesc)] int accountId,
        CancellationToken cancellationToken)
    {
        var request = new GetAccountRequest(customerId, accountId);

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
