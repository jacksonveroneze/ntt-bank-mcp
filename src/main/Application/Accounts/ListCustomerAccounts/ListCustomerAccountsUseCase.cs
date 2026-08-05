using JacksonVeroneze.NET.Result;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Extensions;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Accounts.ListCustomerAccounts;

public sealed class ListCustomerAccountsUseCase(
    ILogger<ListCustomerAccountsUseCase> logger,
    IMapper mapper,
    IAccountRepository accountRepository,
    IBranchCacheRepository branchRepository) : IListCustomerAccountsUseCase
{
    public async Task<Result<ListCustomerAccountsResponse>> ExecuteAsync(
        ListCustomerAccountsRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var accounts = await accountRepository.GetAccountsByCustomerIdAsync(
            request.CustomerId,
            request.AccountType,
            request.Status,
            cancellationToken);

        if (accounts.Count is 0)
        {
            logger.LogEmptyResult(
                nameof(ListCustomerAccountsUseCase),
                nameof(ExecuteAsync),
                request.CustomerId);
        }

        var tasks = accounts
            .Select(a => a.BranchId)
            .Distinct()
            .Select(async branchId =>
                await branchRepository.GetByIdAsync(branchId, cancellationToken));

        var branches = await Task.WhenAll(tasks);

        var response = mapper
            .Map<IReadOnlyCollection<AccountResult>,
                ListCustomerAccountsResponse>(accounts);

        return Result<ListCustomerAccountsResponse>
            .WithSuccess(response);
    }
}
