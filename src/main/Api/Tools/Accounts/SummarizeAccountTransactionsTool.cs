using System.ComponentModel;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NttBankMcp.Api.Extensions;
using NttBankMcp.Api.Security;
using NttBankMcp.Application.Accounts.SummarizeAccountTransactions;

namespace NttBankMcp.Api.Tools.Accounts;

[McpServerToolType]
public sealed class SummarizeAccountTransactionsTool(
    IValidator<SummarizeAccountTransactionsRequest> validator,
    ISummarizeAccountTransactionsUseCase useCase)
{
    #region constants

    private const string SummarizeAccountTransactionsToolName = "summarize_account_transactions";
    private const string SummarizeAccountTransactionsToolTitle = "Summarize Account Transactions";

    private const string SummarizeAccountTransactionsToolDesc =
        """
        Returns TOTALS and aggregated statistics for the transactions of ONE account,
        calculated server-side — count, sum, average, minimum, maximum, and
        share (%) per group. Groups by one of these fields (group_by):
        txn_type, channel, merchant_category, month, or day. Accepts an optional
        period filter (from/to).

        ALWAYS use when the question asks for consolidated numbers or trends —
        e.g., "how much did I spend on X", "total by category", "monthly average",
        "where do I spend the most", "spending by channel", "trend over the period".
        This is the correct and cheap way to get any sum or count.

        DO NOT use list_account_transactions to calculate totals or counts: this
        account has a very high transaction volume, and listing row by row to sum
        is incorrect and expensive. list_account_transactions is only for viewing
        individual entries, not for aggregating.

        Note: amount has no sign; inflow/outflow direction is only distinguished
        when group_by=txn_type. Requires a known account_id.
        """;

    private const string GroupByParamDesc =
        """
        Field to group and aggregate transactions by. Allowed values: 'txn_type'
        (transaction type — the only value that distinguishes inflow from outflow),
        'channel' (transaction channel), 'merchant_category' (merchant category),
        'month' (calendar month) or 'day' (calendar day).
        """;

    private const string FromParamDesc =
        "Optional inclusive start of the period to summarize (UTC). " +
        "Omit to include transactions from the earliest available date.";

    private const string ToDateParamDesc =
        "Optional inclusive end of the period to summarize (UTC). " +
        "Omit to include transactions up to the most recent one.";

    #endregion

    [McpServerTool(
        Name = SummarizeAccountTransactionsToolName,
        Title = SummarizeAccountTransactionsToolTitle)]
    [Description(SummarizeAccountTransactionsToolDesc)]
    [Authorize(Policy = AuthorizationPolicies.AccountTransactionsRead)]
    public async Task<CallToolResult> SummarizeTransactionsAsync(
        [Description(SharedToolConstants.AccountIdParamDesc)] int accountId,
        [Description(GroupByParamDesc)] string groupBy,
        CancellationToken cancellationToken,
        [Description(FromParamDesc)] DateTime? from = null,
        [Description(ToDateParamDesc)] DateTime? toDate = null)
    {
        var request = new SummarizeAccountTransactionsRequest(
            accountId, groupBy, from, toDate);

        var requestValidation = await validator
            .ValidateAsync(request, cancellationToken);

        if (!requestValidation.IsValid)
        {
            return requestValidation.ToCallToolResultError();
        }

        var result = await useCase
            .ExecuteAsync(request, cancellationToken);

        return result.ToCallToolResult();
    }
}
