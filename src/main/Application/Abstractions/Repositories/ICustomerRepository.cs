using NttBank.Mcp.Domain.Results;

namespace NttBank.Mcp.Application.Abstractions.Repositories;

public interface ICustomerRepository
{
    Task<CustomerResult?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);
}
