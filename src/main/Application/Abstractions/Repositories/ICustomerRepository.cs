using NttBankMcp.Domain.Results;
using NttBankMcp.Domain.Results.Common;

namespace NttBankMcp.Application.Abstractions.Repositories;

public interface ICustomerRepository
{
    Task<CustomerResult?> GetByIdAsync(
        int customerId,
        CancellationToken cancellationToken);

    Task<PagedResult<CustomerResult>> GetCustomersAsync(
        int? page,
        int? pageSize,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<RelationshipResult>> GetRelationshipsByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<TicketResult>> GetTicketsByCustomerIdAsync(
        int customerId,
        string? status,
        string? type,
        CancellationToken cancellationToken);
}
