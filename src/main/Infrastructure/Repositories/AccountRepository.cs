using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.Extensions.Logging;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Extensions;
using NttBankMcp.Domain.Enums;
using NttBankMcp.Domain.Results;
using NttBankMcp.Infrastructure.HttpClients;
using Refit;

namespace NttBankMcp.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public sealed class AccountRepository(
    INttBankApi api,
    ILogger<AccountRepository> logger) : IAccountRepository
{
    public async Task<IReadOnlyCollection<AccountResult>> GetAccountsByCustomerIdAsync(
        int customerId,
        AccountType? accountType,
        AccountStatus? status,
        CancellationToken cancellationToken)
    {
        var result = await api.GetCustomerAccountsAsync(
            customerId, accountType, status,
            hasBalance: null, cancellationToken);

        logger.LogCollectionResult(
            nameof(AccountRepository),
            nameof(GetAccountsByCustomerIdAsync),
            customerId,
            result.Count);

        return result;
    }

    public async Task<AccountResult?> GetAccountByIdAsync(
        int accountId,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await api.GetAccountByIdAsync(
                accountId, cancellationToken);

            return result;
        }
        catch (ApiException ex)
            when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            logger.LogNotFound(
                nameof(AccountRepository),
                nameof(GetAccountByIdAsync),
                accountId);

            return null;
        }
    }

    public async Task<IReadOnlyCollection<AccountTransactionResult>>
        GetTransactionsByAccountIdAsync(
            int accountId,
            CancellationToken cancellationToken)
    {
        var result = await api.GetTransactionsByAccountIdAsync(
            accountId, cancellationToken);

        logger.LogCollectionResult(
            nameof(AccountRepository),
            nameof(GetTransactionsByAccountIdAsync),
            accountId,
            result.Count);

        return result;
    }

    public async Task<TransactionSummaryResult> GetTransactionsSummaryByAccountIdAsync(
        int accountId,
        string groupBy,
        DateTime? from,
        DateTime? toDate,
        CancellationToken cancellationToken)
    {
        var result = await api.SummarizeAccountTransactionsAsync(
            accountId, groupBy, from, toDate, cancellationToken);

        return result;
    }
}
