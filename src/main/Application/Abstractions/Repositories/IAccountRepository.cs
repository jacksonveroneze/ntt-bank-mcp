using NttBankMcp.Domain.Enums;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Abstractions.Repositories;

public interface IAccountRepository
{
    Task<IReadOnlyCollection<AccountResult>> GetAccountsByCustomerIdAsync(
        int customerId,
        AccountType? accountType,
        AccountStatus? status,
        CancellationToken cancellationToken);

    Task<AccountResult?> GetAccountByIdAsync(
        int customerId,
        int accountId,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<AccountTransactionResult>> GetTransactionsByAccountIdAsync(
        int customerId,
        int accountId,
        CancellationToken cancellationToken);
}
