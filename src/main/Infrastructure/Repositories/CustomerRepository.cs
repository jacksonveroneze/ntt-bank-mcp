using System.Diagnostics.CodeAnalysis;
using System.Net;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Domain.Results;
using NttBankMcp.Infrastructure.HttpClients;
using Refit;

namespace NttBankMcp.Infrastructure.Repositories;

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
