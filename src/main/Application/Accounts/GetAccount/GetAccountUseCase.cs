using JacksonVeroneze.NET.Result;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Accounts.Common;
using NttBankMcp.Application.Extensions;
using NttBankMcp.Domain.Errors;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Accounts.GetAccount;

public sealed class GetAccountUseCase(
    ILogger<GetAccountUseCase> logger,
    IMapper mapper,
    IAccountRepository repository) : IGetAccountUseCase
{
    public async Task<Result<AccountResponse>> ExecuteAsync(
        GetAccountRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var account = await repository.GetAccountByIdAsync(
            request.AccountId, cancellationToken);

        if (account is null)
        {
            var error = DomainErrors.AccountError.NotFound;

            logger.LogNotFound(nameof(GetAccountUseCase),
                nameof(ExecuteAsync), request.AccountId);

            return Result<AccountResponse>
                .FromNotFound(error);
        }

        var response = mapper
            .Map<AccountResult, AccountResponse>(account);

        return Result<AccountResponse>
            .WithSuccess(response);
    }
}
