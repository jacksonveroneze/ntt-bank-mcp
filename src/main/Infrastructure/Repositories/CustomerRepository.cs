using System.Diagnostics.CodeAnalysis;
using System.Net;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Domain.Enums;
using NttBankMcp.Domain.Results;
using NttBankMcp.Infrastructure.HttpClients;
using Refit;

namespace NttBankMcp.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public sealed class CustomerRepository(
    INttBankApi api) : ICustomerRepository
{
    public async Task<CustomerResult?> GetByIdAsync(
        int customerId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await api.GetCustomerByIdAsync(
                customerId, cancellationToken);

            return result;
        }
        catch (ApiException ex)
            when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IReadOnlyCollection<CustomerAccountResult>> GetAccountsAsync(
        int customerId,
        AccountType? accountType,
        AccountStatus? status,
        CancellationToken cancellationToken)
    {
        var result = await api.GetCustomerAccountsAsync(
            customerId, accountType, status, cancellationToken);

        return result;
    }
}
