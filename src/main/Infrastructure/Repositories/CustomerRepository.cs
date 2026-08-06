using System.Diagnostics.CodeAnalysis;
using System.Net;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Domain.Results;
using NttBankMcp.Domain.Results.Common;
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

    public async Task<PagedResult<CustomerResult>> GetCustomersAsync(
        int? page,
        int? pageSize,
        CancellationToken cancellationToken)
    {
        var result = await api.GetCustomersAsync(
            page, pageSize, cancellationToken);

        return result;
    }

    public async Task<IReadOnlyCollection<RelationshipResult>> GetRelationshipsByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken)
    {
        var result = await api.GetCustomerRelationshipsAsync(
            customerId, cancellationToken);

        return result;
    }

    public async Task<IReadOnlyCollection<TicketResult>> GetTicketsByCustomerIdAsync(
        int customerId,
        string? status,
        string? type,
        CancellationToken cancellationToken)
    {
        var result = await api.GetCustomerTicketsAsync(
            customerId, status, type, cancellationToken);

        return result;
    }
}
