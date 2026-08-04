using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Abstractions.Repositories;

public interface ICardRepository
{
    Task<IReadOnlyCollection<CardResult>> GetCardsByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken);
}
