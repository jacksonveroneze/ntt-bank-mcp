using System.ComponentModel;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NttBankMcp.Api.Extensions;
using NttBankMcp.Api.Security;
using NttBankMcp.Application.Accounts.ListAccountTransactions;

namespace NttBankMcp.Api.Tools.Accounts;

[McpServerToolType]
public sealed class ListAccountTransactionsTool(
    IValidator<ListAccountTransactionsRequest> validator,
    IListAccountTransactionsUseCase useCase)
{
    #region constants

    private const string ListAccountTransactionsToolName = "list_account_transactions";
    private const string ListAccountTransactionsToolTitle = "List Account Transactions";

    private const string ListAccountTransactionsToolDesc =
        """
        Returns the full list of transactions (individual entries) for ONE specific
        bank account, given its account_id. Each entry includes its date, type,
        amount, channel and merchant category. This call returns every transaction
        for the account in one response — it does not support date-range, type or
        channel filtering, and it is not paginated.

        Use this when the user wants to see account movements line by line, e.g.
        "list the transactions", "show the statement", "recent entries for this
        account". Once you have the list, you can filter or search within it
        yourself if the user asked for a specific period, type or channel.

        Do NOT use this for totals, sums, averages, counts or trends — for any
        aggregation use summarize_account_transactions instead. Some accounts may
        have a large number of transactions; fetching everything just to compute a
        total is unnecessary and expensive.

        Requires a known account_id. If you only have the customer, resolve their
        accounts first.
        """;

    #endregion

    [McpServerTool(
        Name = ListAccountTransactionsToolName,
        Title = ListAccountTransactionsToolTitle)]
    [Description(ListAccountTransactionsToolDesc)]
    [Authorize(Policy = AuthorizationPolicies.AccountTransactionsRead)]
    public async Task<CallToolResult> ListTransactionsAsync(
        [Description(SharedToolConstants.AccountIdParamDesc)] int accountId,
        CancellationToken cancellationToken)
    {
        var request = new ListAccountTransactionsRequest(accountId);

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
