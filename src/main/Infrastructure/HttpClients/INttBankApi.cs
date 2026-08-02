using NttBankMcp.Domain.Enums;
using NttBankMcp.Domain.Results;
using Refit;

namespace NttBankMcp.Infrastructure.HttpClients;

public interface INttBankApi
{
    [Get("/customers/{id}")]
    Task<CustomerResult> GetCustomerByIdAsync(
        int id,
        CancellationToken cancellationToken);
    
    [Get("/customers/{id}/accounts")]
    Task<IReadOnlyCollection<CustomerAccountResult>> GetCustomerAccountsAsync(
        int id,
        [Query] AccountType? accountType,
        [Query] AccountStatus? status,
        CancellationToken cancellationToken);
}
