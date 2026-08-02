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
    ICustomerRepository repository) : IGetCustomerAccountsUseCase
{
    public async Task<Result<GetCustomerAccountsResponse>> ExecuteAsync(
        GetCustomerAccountsRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var accounts = await repository.GetAccountsAsync(
            request.CustomerId, cancellationToken);

        if (accounts.Count is 0)
        {
            logger.LogEmptyResult(
                nameof(GetCustomerAccountsUseCase),
                nameof(ExecuteAsync),
                request.CustomerId);
        }

        var response = mapper
            .Map<IReadOnlyCollection<CustomerAccountResult>,
                GetCustomerAccountsResponse>(accounts);

        return Result<GetCustomerAccountsResponse>
            .WithSuccess(response);
    }
}
