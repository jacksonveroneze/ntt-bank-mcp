using NttBankMcp.Domain.Enums;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Abstractions.Repositories;

public interface ICustomerRepository
{
    Task<CustomerResult?> GetByIdAsync(
        int customerId,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<AccountResult>> GetAccountsAsync(
        int customerId,
        AccountType? accountType,
        AccountStatus? status,
        CancellationToken cancellationToken);
}
