using JacksonVeroneze.NET.Result;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Extensions;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Customers.GetCustomerAccounts;

public sealed class GetCustomerAccountsUseCase(
    ILogger<GetCustomerAccountsUseCase> logger,
    IMapper mapper,
    IAccountRepository accountRepository,
    IBranchRepository branchRepository) : IGetCustomerAccountsUseCase
{
    public async Task<Result<GetCustomerAccountsResponse>> ExecuteAsync(
        GetCustomerAccountsRequest request,
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
                nameof(GetCustomerAccountsUseCase),
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
                GetCustomerAccountsResponse>(accounts);

        return Result<GetCustomerAccountsResponse>
            .WithSuccess(response);
    }
}
