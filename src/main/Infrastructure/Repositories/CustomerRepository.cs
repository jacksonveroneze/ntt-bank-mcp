using System.Diagnostics.CodeAnalysis;
using System.Net;
using NttBank.Mcp.Application.Abstractions.Repositories;
using NttBank.Mcp.Domain.Results;
using NttBank.Mcp.Infrastructure.HttpClients;
using Refit;

namespace NttBank.Mcp.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public sealed class CustomerRepository(
    INttBankApi api): ICustomerRepository
{
    public async Task<CustomerResult?> GetByIdAsync(
        int id, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await api.GetCustomerByIdAsync(
                id, cancellationToken);

            return customer;
        }
        catch (ApiException ex) 
            when(ex.StatusCode is HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}
