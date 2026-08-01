using NttBankMcp.Domain.Results;
using Refit;

namespace NttBankMcp.Infrastructure.HttpClients;

public interface INttBankApi
{
    [Get("/customers/{id}")]
    Task<CustomerResult> GetCustomerByIdAsync(
        int id,
        CancellationToken cancellationToken);
}
