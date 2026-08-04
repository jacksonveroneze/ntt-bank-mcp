using JacksonVeroneze.NET.Result;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Extensions;
using NttBankMcp.Domain.Errors;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Accounts.ListAccountTransactions;

public sealed class ListAccountTransactionsUseCase(
    ILogger<ListAccountTransactionsUseCase> logger,
    IMapper mapper,
    ICustomerRepository repository) : IListAccountTransactionsUseCase
{
    public async Task<Result<ListAccountTransactionsResponse>> ExecuteAsync(
        ListAccountTransactionsRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var account = await repository.GetAccountByIdAsync(
            request.CustomerId, request.AccountId, cancellationToken);

        if (account is null)
        {
            var error = DomainErrors.AccountError.NotFound;

            logger.LogNotFound(nameof(ListAccountTransactionsUseCase),
                nameof(ExecuteAsync), request.CustomerId, request.AccountId);

            return Result<ListAccountTransactionsResponse>
                .FromNotFound(error);
        }

        var transactions = await repository.GetTransactionsByAccountIdAsync(
            request.CustomerId, request.AccountId, cancellationToken);

        if (transactions.Count is 0)
        {
            logger.LogEmptyResult(
                nameof(ListAccountTransactionsUseCase),
                nameof(ExecuteAsync),
                request.AccountId);
        }

        var response = mapper
            .Map<IReadOnlyCollection<AccountTransactionResult>,
                ListAccountTransactionsResponse>(transactions);

        return Result<ListAccountTransactionsResponse>
            .WithSuccess(response);
    }
}
