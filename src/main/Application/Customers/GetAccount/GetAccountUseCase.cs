using JacksonVeroneze.NET.Result;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Extensions;
using NttBankMcp.Domain.Errors;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Customers.GetAccount;

public sealed class GetAccountUseCase(
    ILogger<GetAccountUseCase> logger,
    IMapper mapper,
    ICustomerRepository repository) : IGetAccountUseCase
{
    public async Task<Result<GetAccountResponse>> ExecuteAsync(
        GetAccountRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var account = await repository.GetAccountByIdAsync(
            request.CustomerId, request.AccountId, cancellationToken);

        if (account is null)
        {
            var error = DomainErrors.AccountError.NotFound;

            logger.LogNotFound(nameof(GetAccountUseCase),
                nameof(ExecuteAsync), request.CustomerId, request.AccountId);

            return Result<GetAccountResponse>
                .FromNotFound(error);
        }

        var response = mapper
            .Map<AccountResult, GetAccountResponse>(account);

        return Result<GetAccountResponse>
            .WithSuccess(response);
    }
}
