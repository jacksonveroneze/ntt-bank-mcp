using NttBank.Mcp.Domain.Results;
using Refit;

namespace NttBank.Mcp.Infrastructure.HttpClients;

public interface INttBankApi
{
    [Get("/customers/{id}")]
    Task<CustomerResult> GetCustomerByIdAsync(
        int id,
        CancellationToken cancellationToken);
}
