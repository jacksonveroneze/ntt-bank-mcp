using NttBank.Mcp.Domain.Entities;

namespace NttBank.Mcp.Application.Abstractions.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);
}
