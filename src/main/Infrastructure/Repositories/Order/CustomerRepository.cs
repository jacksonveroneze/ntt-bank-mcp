using System.Diagnostics.CodeAnalysis;
using MapsterMapper;
using NttBank.Mcp.Application.Abstractions.Repositories;
using NttBank.Mcp.Domain.Entities;
using NttBank.Mcp.Infrastructure.HttpClients;

namespace NttBank.Mcp.Infrastructure.Repositories.Order;

[ExcludeFromCodeCoverage]
public sealed class CustomerRepository(
    IMapper mapper,
    INttBankApi api): ICustomerRepository
{
    public async Task<Customer?> GetByIdAsync(
        int id, CancellationToken cancellationToken)
    {
        var customer = await api.GetCustomerByIdAsync(
            id, cancellationToken);

        var result = mapper.Map<Customer>(customer!);
        
        return result;
    }
}
