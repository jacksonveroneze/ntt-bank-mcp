using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Abstractions.Repositories;

public interface ICustomerRepository
{
    Task<CustomerResult?> GetByIdAsync(
        int customerId,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<CustomerAccountResult>?> GetAccountsAsync(
        int customerId,
        CancellationToken cancellationToken);
}
