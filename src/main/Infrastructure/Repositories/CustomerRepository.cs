using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.Extensions.Logging;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Extensions;
using NttBankMcp.Domain.Results;
using NttBankMcp.Domain.Results.Common;
using NttBankMcp.Infrastructure.HttpClients;
using Refit;

namespace NttBankMcp.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public sealed class CustomerRepository(
    INttBankApi api,
    ILogger<CustomerRepository> logger) : ICustomerRepository
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
            logger.LogNotFound(
                nameof(CustomerRepository),
                nameof(GetByIdAsync), 
                customerId);

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

        logger.LogCollectionResult(
            nameof(CustomerRepository),
            nameof(GetCustomersAsync), 
            result.Items.Count);

        return result;
    }

    public async Task<IReadOnlyCollection<RelationshipResult>> GetRelationshipsByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken)
    {
        var result = await api.GetCustomerRelationshipsAsync(
            customerId, cancellationToken);

        logger.LogCollectionResult(
            nameof(CustomerRepository),
            nameof(GetRelationshipsByCustomerIdAsync), 
            customerId, 
            result.Count);

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

        logger.LogCollectionResult(
            nameof(CustomerRepository),
            nameof(GetTicketsByCustomerIdAsync), 
            customerId, 
            result.Count);

        return result;
    }
}
