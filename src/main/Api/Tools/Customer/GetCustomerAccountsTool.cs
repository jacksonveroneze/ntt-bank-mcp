using System.ComponentModel;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NttBankMcp.Api.Extensions;
using NttBankMcp.Api.Security;
using NttBankMcp.Application.Customers.GetCustomerAccounts;
using NttBankMcp.Domain.Enums;

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
        Lists the bank accounts owned by a single customer, identified by their
        unique numeric ID. Each account includes its account ID, branch ID, account
        type, balance, open date and status. Results can be narrowed by account type
        and by account status; both filters are optional and combine with AND when
        used together. A customer with no matching accounts is a valid outcome: the
        tool succeeds and returns an empty account list. Use this tool when you need
        the accounts, balances or account status of a known customer.
        """;

    private const string AccountTypeParamDesc =
        """
        Optional filter by account type. Allowed values: 'FixedDeposit' (term
        investment account), 'Current' (everyday checking account) and 'Salary'
        (payroll account). Omit to return accounts of every type.
        """;

    private const string StatusParamDesc =
        """
        Optional filter by account status. Allowed values: 'Active' (open and
        usable), 'Blocked' (temporarily restricted) and 'Closed' (permanently
        terminated). Omit to return accounts in every status. Prefer 'Active' when
        the question is about available balance or accounts currently in use, so
        that closed accounts are not counted.
        """;

    #endregion

    [McpServerTool(
        Name = GetCustomerAccountsToolName,
        Title = GetCustomerAccountsToolTitle)]
    [Description(GetCustomerAccountsToolDesc)]
    [Authorize(Policy = AuthorizationPolicies.CustomerAccountsRead)]
    public async Task<CallToolResult> GetAccountsAsync(
        [Description(CustomerToolConstants.CustomerIdParamDesc)] int customerId,
        CancellationToken cancellationToken,
        [Description(AccountTypeParamDesc)] AccountType? accountType = null,
        [Description(StatusParamDesc)] AccountStatus? status = null)
    {
        var request = new GetCustomerAccountsRequest(
            customerId, accountType, status);

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
