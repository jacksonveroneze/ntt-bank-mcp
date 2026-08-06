using JacksonVeroneze.NET.Result;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Extensions;
using NttBankMcp.Domain.Errors;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Accounts.SummarizeAccountTransactions;

public sealed class SummarizeAccountTransactionsUseCase(
    ILogger<SummarizeAccountTransactionsUseCase> logger,
    IMapper mapper,
    IAccountRepository repository) : ISummarizeAccountTransactionsUseCase
{
    public async Task<Result<SummarizeAccountTransactionsResponse>> ExecuteAsync(
        SummarizeAccountTransactionsRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var account = await repository.GetAccountByIdAsync(
            request.AccountId, cancellationToken);

        if (account is null)
        {
            var error = DomainErrors.AccountError.NotFound;

            logger.LogNotFound(nameof(SummarizeAccountTransactionsUseCase),
                nameof(ExecuteAsync), request.AccountId);

            return Result<SummarizeAccountTransactionsResponse>
                .FromNotFound(error);
        }

        var summary = await repository.GetTransactionsSummaryByAccountIdAsync(
            request.AccountId, request.GroupBy, request.From,
            request.ToDate, cancellationToken);

        var response = mapper
            .Map<TransactionSummaryResult, SummarizeAccountTransactionsResponse>(summary);

        return Result<SummarizeAccountTransactionsResponse>
            .WithSuccess(response);
    }
}
